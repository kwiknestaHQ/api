using KwikNesta.Shared.Helpers;
using KwikNesta.Shared.Models.Enumerations.Infra;
using KwikNesta.Shared.Models.Settings;
using KwikNesta.Shared.ServiceDTOs.Infra;
using KwikNestaInfra.Infrastructure.Contracts;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace KwikNestaInfra.Infrastructure.Services
{
    public class AgoraWebhookEventDispatcher(IEnumerable<IAgoraModuleHandler> handlers,
                                        ILogger<AgoraWebhookEventDispatcher> logger,
                                        IOptions<KNApplicationSettings> options)
    {
        private readonly Dictionary<(EAgoraModule, EAgoraEvent), IAgoraModuleHandler> _handlers 
            = handlers.ToDictionary(h => (h.Module, h.Event), h => h);
        private readonly ILogger<AgoraWebhookEventDispatcher> _logger = logger;
        private readonly string _secretKey = options.Value.Jwt.Key;

        public async Task DispatchAsync<T>(AgoraWebhookPayload<T> data) where T : AgoraPayloadBase
        {
            var payload = data.Payload ??
                throw new ArgumentNullException(nameof(data.Payload));
            var channelContext = AgoraChannelName.Decode(payload.ChannelName, _secretKey);

            if (!_handlers.TryGetValue((channelContext.Module, data.Event), out var handler))
            {
                _logger.LogWarning("[DispatchAsync] Unknown Module: {Module}", channelContext.Module);
                return;
            }

            await handler.HandleAsync(data.NoticeId, channelContext.EntityId, payload);
        }
    }
}