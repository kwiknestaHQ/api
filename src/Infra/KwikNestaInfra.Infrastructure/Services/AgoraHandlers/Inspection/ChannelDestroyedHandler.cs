using KwikNesta.Mediator.Cores.Abstractions;
using KwikNesta.Shared.Models.Enumerations.Infra;
using KwikNesta.Shared.ServiceDTOs.Infra;

namespace KwikNestaInfra.Infrastructure.Services.AgoraHandlers.Inspection
{
    public class ChannelDestroyedHandler(IInfraRepositoryManager repository, 
                                    IKNMediator mediator) : AgoraModuleHandler<Agora102Payload>
    {
        private readonly IInfraRepositoryManager _repository = repository;
        private readonly IKNMediator _mediator = mediator;

        public override EAgoraModule Module => EAgoraModule.Inspection;
        public override EAgoraEvent Event => EAgoraEvent.ChannelDestroyed;

        public async override Task HandleAsync(string noticeId, string entityId, Agora102Payload payload)
        {
            var model = InfraObjectFactory.Initialize(noticeId, 
                            payload.ChannelName, 
                            Event, 
                            payload.Timestamp, 
                            Module, 
                            uid: payload.LastUid);

            var notProcessed = await _repository
                .AgoraWebhookLogs.TryInsertAsync(model);

            if (!notProcessed)
            {
                return;
            }
        }
    }
}