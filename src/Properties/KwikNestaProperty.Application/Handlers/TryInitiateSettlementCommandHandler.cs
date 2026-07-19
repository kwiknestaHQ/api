using KwikNesta.Mediator.Cores.Abstractions;
using KwikNesta.Shared.Models.Enumerations.Infra;
using KwikNesta.Shared.Models.Enumerations.Payments;
using KwikNesta.Shared.Models.Enumerations.Property;
using KwikNesta.Shared.Models.Settings;
using KwikNesta.Shared.Responses;
using KwikNesta.Shared.ServiceCommands.Identity;
using KwikNesta.Shared.ServiceCommands.Payment;
using KwikNesta.Shared.ServiceCommands.Property;
using KwikNesta.Shared.ServiceQueries.Infra;
using KwikNestaProperty.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace KwikNestaProperty.Application.Handlers
{
    public class TryInitiateSettlementCommandHandler(IPropertyRepositoryManager repository, 
            IOptions<KNApplicationSettings> options, 
            IKNMediator mediator) 
        : IKNRequestHandler<TryInitiateSettlementCommand, Response<string>>
    {
        private readonly IPropertyRepositoryManager _repository = repository;
        private readonly IKNMediator _mediator = mediator;
        private readonly SettlementConfig _settlementConfig = options.Value.Settlement ?? 
            throw new ArgumentNullException(nameof(SettlementConfig));
        private readonly AgoraSetting _agoraConfig = options.Value.Agora ??
           throw new ArgumentNullException(nameof(AgoraSetting));
        private readonly ViewingCheckInSettings _settings = options.Value.CheckIn ??
                throw new ArgumentNullException(nameof(ViewingCheckInSettings));

        public async Task<Response<string>> HandleAsync(TryInitiateSettlementCommand request, CancellationToken cancellationToken)
        {
            var session = await _repository.ViewingSession
                .Get(vs => vs.ChannelName == request.Channel)
                .Include(vs => vs.Participants)
                .FirstOrDefaultAsync(cancellationToken);

            if (session == null)
            {
                return Response<string>.Fail(string.Format(PropertyResponse.RecordNotFound, "Viewing Session"),
                    StatusCodes.Status404NotFound);
            }

            if (session.Participants.Count == 0)
            {
                return Response<string>.Fail(string.Format(PropertyResponse.RecordNotFound, "Participants"),
                    StatusCodes.Status404NotFound);
            }

            var landlord = session.Participants
                .FirstOrDefault(sp => sp.Role == ESessionParticipantRole.Publisher);

            var tenant = session.Participants
                .FirstOrDefault(sp => sp.Role == ESessionParticipantRole.Subscriber);

            if (landlord == null || tenant == null)
            {
                return Response<string>.Fail(PropertyResponse.RecordNotFound,
                    StatusCodes.Status404NotFound);
            }

            if (session.IsSettled())
            {
                return Response<string>.Fail(
                    string.Format(PropertyResponse.SettlementHandledForChannel, request.Channel),
                    StatusCodes.Status409Conflict);
            }

            var viewingRequest = await _repository.ViewingRequest
                .FirstOrDefault(v => v.SessionId == session.Id);
            if (viewingRequest == null)
            {
                return Response<string>.Fail(
                    string.Format(PropertyResponse.RecordNotFound, "Viewing Request"),
                    StatusCodes.Status404NotFound);
            }

            var sessionExpiryInSeconds = viewingRequest.Type == EViewingType.Virtual ?
                _agoraConfig.ExpiryInSeconds :
                _settings.WindowMinutesAfter * 60;

            if (!session.IsExpired(sessionExpiryInSeconds))
            {
                return Response<string>.Fail(
                    string.Format(PropertyResponse.SessionNotExpiredYet, request.Channel),
                    StatusCodes.Status403Forbidden);
            }

            var settlementOutcome = InspectionSettlementResult.DetermineSettlement(
                    _settlementConfig, 
                    viewingRequest.Fee, 
                    landlord.JoinedAt.HasValue, 
                    tenant.JoinedAt.HasValue);

            var settlementData = settlementOutcome.LandlordShare > 0.00m ? 
                (landlord.UserId, settlementOutcome.LandlordShare, EPayoutType.Transfer) : 
                (tenant.UserId, settlementOutcome.RefundAmount, EPayoutType.Refund);

            var settlementResult = await _mediator.SendAsync(new CreateSettlementCommand
            {
                Amount = settlementData.Item2,
                UserId = settlementData.UserId,
                Purpose = EPaymentPurpose.Viewing,
                ReferenceId = viewingRequest.Id,
                SettlementType = settlementData.Item3
            }, cancellationToken);

            if (!settlementResult.Success)
            {
                return Response<string>.Fail(settlementResult.Message, settlementResult.StatusCode);
            }

            var sessionCompletionTime = await GetCompletionTime(
                session.ChannelName, 
                session.ViewingRequestId,
                viewingRequest.Type, 
                cancellationToken);

            session.UpdateSettlementStatus(ESSessionettlementStatus.Scheduled);
            session.MarkAsCompleted(sessionCompletionTime);
            if(settlementOutcome.Outcome == ESettlementOutcome.NoShow)
            {
                viewingRequest.MarkNoShow();
            }
            else
            {
                viewingRequest.MarkCompleted();
            }

            await _repository.SaveAsync();

            await RunNoShowUpdates(landlord.UserId, tenant.UserId, settlementOutcome, cancellationToken);
            return Response<string>.Ok(
                string.Format(PropertyResponse.SettlementSuccessfullyScheduled, 
                    request.Channel));
        }

        private async Task RunNoShowUpdates(string landlordId, 
            string tenantId, 
            InspectionSettlementResult settlementResult, 
            CancellationToken cancellationToken)
        {
            switch (settlementResult.Outcome)
            {
                case ESettlementOutcome.NoShow:
                    await _mediator.SendAsync(new RecordNoShowCommand
                    {
                        UserId = landlordId
                    }, cancellationToken);

                    await _mediator.SendAsync(new RecordNoShowCommand
                    {
                        UserId = tenantId
                    }, cancellationToken);
                    break;
                case ESettlementOutcome.LandlordNoShow:
                    await _mediator.SendAsync(new RecordNoShowCommand
                    {
                        UserId = landlordId
                    }, cancellationToken);
                    break;
                case ESettlementOutcome.TenantNoShow:
                    await _mediator.SendAsync(new RecordNoShowCommand
                    {
                        UserId = tenantId
                    }, cancellationToken);
                    break;
            }
        }

        private async Task<DateTime> GetCompletionTime(string channelName, 
                                        Guid requestId, 
                                        EViewingType viewingType, 
                                        CancellationToken cancellationToken)
        {
            var time = DateTime.UtcNow;
            switch (viewingType)
            {
                case EViewingType.Virtual:
                    var eventLog = await _mediator.SendAsync(new GetAgoraWebhookEventLogQuery
                    {
                        Channel = channelName,
                        Event = EAgoraEvent.ChannelDestroyed,
                        Module = EAgoraModule.Inspection
                    }, cancellationToken);
                    
                    time = eventLog.Data?
                        .OrderByDescending(e => e.Timestamp)
                        .FirstOrDefault()?.Timestamp ?? time;
                    break;
                case EViewingType.Physical:
                    time = (await _repository.ViewingCheckIn
                        .Get(c => c.ViewingRequestId == requestId)
                        .OrderByDescending(c => c.CreatedOn)
                        .FirstOrDefaultAsync(cancellationToken))?.CreatedOn ?? time;
                    break;
                default:
                    break;
            }

            return time;
        }
    }
}
