using KwikNesta.Mediator.Cores.Abstractions;
using KwikNesta.Shared.Extensions;
using KwikNesta.Shared.Models.Enumerations.Infra;
using KwikNesta.Shared.Models.Enumerations.Property;
using KwikNesta.Shared.ServiceCommands.Property;
using KwikNesta.Shared.ServiceDTOs.Infra;
using Microsoft.Extensions.Logging;

namespace KwikNestaInfra.Infrastructure.Services.AgoraHandlers.Inspection
{
    public class AudienceJoinsHandler(IInfraRepositoryManager repository, 
                                IKNMediator mediator,
                                ILogger<AudienceJoinsHandler> logger) 
        : AgoraModuleHandler<Agora105Payload>
    {
        private readonly IInfraRepositoryManager _repository = repository;
        private readonly IKNMediator _mediator = mediator;
        private readonly ILogger<AudienceJoinsHandler> _logger = logger;

        public override EAgoraModule Module => EAgoraModule.Inspection;

        public override EAgoraEvent Event => EAgoraEvent.AudienceJoins;

        public async override Task HandleAsync(string noticeId, string entityId, Agora105Payload payload)
        {
            var model = InfraObjectFactory.Initialize(noticeId,
                                payload.ChannelName,
                                Event,
                                payload.Timestamp,
                                Module,
                                payload.EPlatform,
                                payload.Uid);
            var notProcessed = await _repository
                .AgoraWebhookLogs.TryInsertAsync(model);

            if (!notProcessed)
            {
                return;
            }

            var response = await _mediator.SendAsync(new ViewSessionUserJoinedCommand
            {
                ChannelName = payload.ChannelName,
                UId = payload.Uid,
                Timestamp = payload.Timestamp.ToUtcDateTime(),
                Role = ESessionParticipantRole.Subscriber
            });

            if (!response.Success)
            {
                _logger.LogError(response.Message);
            }
            else
            {
                _logger.LogInformation(response.Message);
            }
        }
    }
}