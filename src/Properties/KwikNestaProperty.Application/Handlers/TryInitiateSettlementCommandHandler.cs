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

            if (session.IsSettled())
            {
                return Response<string>.Fail(
                    string.Format(PropertyResponse.SettlementHandledForChannel, request.Channel),
                    StatusCodes.Status409Conflict);
            }

            if (!session.IsExpired(_agoraConfig.ExpiryInSeconds))
            {
                return Response<string>.Fail(
                    string.Format(PropertyResponse.SessionNotExpiredYet, request.Channel),
                    StatusCodes.Status403Forbidden);
            }

            var viewingRequest = await _repository.ViewingRequest
                .FirstOrDefault(v => v.SessionId == session.Id);
            if (viewingRequest == null)
            {
                return Response<string>.Fail(
                    string.Format(PropertyResponse.RecordNotFound, "Viewing Request"),
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

            var eventLog = await _mediator.SendAsync(new GetAgoraWebhookEventLogQuery
            {
                Channel = session.ChannelName,
                Event = EAgoraEvent.ChannelDestroyed,
                Module = EAgoraModule.Inspection
            }, cancellationToken);

            var sessionCompletedTime = eventLog.Data?
                .OrderByDescending(e => e.Timestamp)
                .FirstOrDefault()?.Timestamp ?? DateTime.UtcNow;

            session.UpdateSettlementStatus(ESSessionettlementStatus.Scheduled);
            session.MarkAsCompleted(sessionCompletedTime);
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
    }
}