using KwikNesta.Mediator.Cores.Abstractions;
using KwikNesta.Shared.Extensions;
using KwikNesta.Shared.Models.Enumerations.Infra;
using KwikNesta.Shared.ServiceCommands.Property;
using KwikNesta.Shared.ServiceDTOs.Infra;
using Microsoft.Extensions.Logging;

namespace KwikNestaInfra.Infrastructure.Services.AgoraHandlers.Inspection
{
    public class ChannelCreatedHandler(IInfraRepositoryManager repository, 
                                    IKNMediator mediator,
                                    ILogger<ChannelCreatedHandler> logger) 
        : AgoraModuleHandler<Agora101Payload>
    {
        private readonly IInfraRepositoryManager _repository = repository;
        private readonly IKNMediator _mediator = mediator;
        private readonly ILogger<ChannelCreatedHandler> _logger = logger;

        public override EAgoraModule Module => EAgoraModule.Inspection;
        public override EAgoraEvent Event => EAgoraEvent.ChannelCreated;

        public override async Task HandleAsync(string noticeId, string entityId, Agora101Payload payload)
        {
            var model = InfraObjectFactory.Initialize(noticeId, payload.ChannelName, Event, payload.Timestamp, Module);
            var notProcessed = await _repository
                .AgoraWebhookLogs.TryInsertAsync(model);

            if (!notProcessed)
            {
                return;
            }

            var response = await _mediator.SendAsync(new ViewSessionStartedCommand
            {
                ChannelName = payload.ChannelName,
                Timestamp = payload.Timestamp.ToUtcDateTime()
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