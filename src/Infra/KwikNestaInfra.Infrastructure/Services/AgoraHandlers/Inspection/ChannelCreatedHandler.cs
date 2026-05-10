using KwikNesta.Shared.Models.Enumerations.Infra;
using KwikNesta.Shared.ServiceDTOs.Infra;
using KwikNestaInfra.Infrastructure.Contracts;

namespace KwikNestaInfra.Infrastructure.Services.AgoraHandlers.Inspection
{
    public class ChannelCreatedHandler(IInfraRepositoryManager repository) 
        : IAgoraModuleHandler
    {
        private readonly IInfraRepositoryManager _repository = repository;

        public EAgoraModule Module => EAgoraModule.Inspection;

        public EAgoraEvent Event => EAgoraEvent.ChannelCreated;

        public async Task HandleAsync(string noticeId, string entityId, AgoraPayloadBase payload)
        {
            var model = InfraObjectFactory.Initialize(noticeId, payload.ChanelName, Event, payload.Timestamp, Module);
            var processed = await _repository
                .AgoraWebhookLogs.TryInsertAsync(model);

            if (!processed)
            {
                return;
            }
        }
    }
}