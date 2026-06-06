using KwikNesta.Mediator.Cores.Abstractions;
using KwikNesta.Shared.Extensions;
using KwikNesta.Shared.Models.Enumerations.Infra;
using KwikNesta.Shared.Models.Enumerations.Property;
using KwikNesta.Shared.ServiceCommands.Property;
using KwikNesta.Shared.ServiceDTOs.Infra;
using Microsoft.Extensions.Logging;

namespace KwikNestaInfra.Infrastructure.Services.AgoraHandlers.Inspection
{
    public class HostLeavesHandler(IInfraRepositoryManager repository, 
                                IKNMediator mediator,
                                ILogger<HostLeavesHandler> logger) 
        : AgoraModuleHandler<Agora104Payload>
    {
        private readonly IInfraRepositoryManager _repository = repository;
        private readonly IKNMediator _mediator = mediator;
        private readonly ILogger<HostLeavesHandler> _logger = logger;

        public override EAgoraModule Module => EAgoraModule.Inspection;
        public override EAgoraEvent Event => EAgoraEvent.HostLeaves;

        public async override Task HandleAsync(string noticeId, string entityId, Agora104Payload payload)
        {
            var model = InfraObjectFactory.Initialize(noticeId,
                                payload.ChannelName,
                                Event,
                                payload.Timestamp,
                                Module,
                                payload.EPlatform,
                                payload.Uid,
                                payload.Duration,
                                payload.Reason);

            var notProcessed = await _repository
                .AgoraWebhookLogs.TryInsertAsync(model);

            if (!notProcessed)
            {
                return;
            }

            var response = await _mediator.SendAsync(new ViewSessionUserLeftCommand
            {
                ChannelName = payload.ChannelName,
                UId = payload.Uid,
                Timestamp = payload.Timestamp.ToUtcDateTime(),
                Role = ESessionParticipantRole.Publisher,
                Duration = payload.Duration
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