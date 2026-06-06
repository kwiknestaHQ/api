using KwikNesta.Mediator.Cores.Abstractions;
using KwikNesta.Shared.Implementations;
using KwikNesta.Shared.Models.Enumerations.Infra;
using KwikNesta.Shared.Models.Enumerations.Property;
using KwikNesta.Shared.Models.Settings;
using KwikNesta.Shared.Responses;
using KwikNesta.Shared.ServiceCommands.Infra;
using KwikNesta.Shared.ServiceCommands.Property;
using KwikNesta.Shared.ServiceDTOs.Property;
using KwikNesta.Shared.ServiceQueries.Infra;
using KwikNestaProperty.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace KwikNestaProperty.Application.Handlers
{
    public class JoinViewSessionCallCommandHandler(IPropertyRepositoryManager repository, 
                            IKNMediator mediator, 
                            IOptions<KNApplicationSettings> options) :
        IKNRequestHandler<JoinViewSessionCallCommand, Response<AgoraSessionTokenDto>>
    {
        private readonly IPropertyRepositoryManager _repository = repository;
        private readonly IKNMediator _mediator = mediator;
        private readonly AgoraSetting _agoraSetting = options.Value.Agora ?? 
            throw new ArgumentNullException(nameof(AgoraSetting));

        public async Task<Response<AgoraSessionTokenDto>> HandleAsync(JoinViewSessionCallCommand request, 
            CancellationToken cancellationToken)
        {
            if (request.SessionId == Guid.Empty 
                || request.Context == null || string.IsNullOrWhiteSpace(request.Context.Id))
            {
                return Response<AgoraSessionTokenDto>.Fail(PropertyResponse.InvalidRequest,
                    StatusCodes.Status400BadRequest);
            }

            var session = await _repository.ViewingSession
                .Get(s => s.Id == request.SessionId)
                .Include(s => s.Participants)
                .FirstOrDefaultAsync(cancellationToken);

            if (session == null)
            {
                return Response<AgoraSessionTokenDto>.Fail(
                    string.Format(PropertyResponse.RecordNotFound, "View Session"),
                    StatusCodes.Status404NotFound);
            }

            var participant = session.Participants
                .FirstOrDefault(p => p.UserId == request.Context.Id);

            if (participant == null)
            {
                return Response<AgoraSessionTokenDto>.Fail(
                    string.Format(PropertyResponse.NotAllowedToJoinNotAPaticipant),
                    StatusCodes.Status403Forbidden);
            }

            var existingToken = await _mediator.SendAsync(new GetAgoraTokenQuery
            {
                Channel = session.ChannelName,
                UserId = participant.UserId
            }, cancellationToken);

            if(!existingToken.Success && existingToken.StatusCode != StatusCodes.Status404NotFound)
            {
                return Response<AgoraSessionTokenDto>.Fail(existingToken.Message, existingToken.StatusCode);
            }

            if(existingToken.Success && existingToken.Data != null)
            {
                return Response<AgoraSessionTokenDto>.Ok(new AgoraSessionTokenDto
                {
                    AppId = _agoraSetting.AppId,
                    Channel = existingToken.Data.ChannelName,
                    UId = participant.UId,
                    Token = existingToken.Data.Token
                });
            }

            var newTokenResult = await _mediator.SendAsync(new GenerateAgoraRtcTokenCommand
            {
                ChannelName = session.ChannelName,
                IsPublisher = participant.Role == ESessionParticipantRole.Publisher,
                SessionScheduledTime = session.ScheduledStart,
                UId = participant.UId
            }, cancellationToken);

            if (!newTokenResult.Success)
            {
                return Response<AgoraSessionTokenDto>.Fail(newTokenResult.Message, newTokenResult.StatusCode);
            }

            var saveTokenResult = await _mediator.SendAsync(new CreateAgoraTokenCommand
            {
                Channel = session.ChannelName,
                Token = newTokenResult.Data.Token,
                Expires = session.ScheduledStart
                    .AddSeconds(_agoraSetting.ExpiryInSeconds),
                UserId = participant.UserId
            }, cancellationToken);

            if (!saveTokenResult.Success)
            {
                return Response<AgoraSessionTokenDto>.Fail(saveTokenResult.Message, newTokenResult.StatusCode);
            }

            AppAudit.Write(request.Context.Id,
                request.Context.Email,
                EAuditAction.JoinedViewSession,
                EAuditDomain.ViewRequest,
                session.ViewingRequestId.ToString(),
                request.Context.IpAddress);

            return Response<AgoraSessionTokenDto>.Ok(new AgoraSessionTokenDto
            {
                AppId = _agoraSetting.AppId,
                Channel = session.ChannelName,
                Token = newTokenResult.Data.Token,
                UId = participant.UId
            });
        }
    }
}