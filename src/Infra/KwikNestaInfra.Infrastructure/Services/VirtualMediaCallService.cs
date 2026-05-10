using Hangfire.Console;
using Hangfire.Server;
using KwikNesta.Shared.Models.Enumerations.Infra;
using KwikNesta.Shared.ServiceDTOs.Infra;
using KwikNestaInfra.Infrastructure.Contracts;
using System.Text.Json;

namespace KwikNestaInfra.Infrastructure.Services
{
    public class VirtualMediaCallService(AgoraWebhookEventDispatcher dispatcher) : IVirtualMediaCallService
    {
        private readonly AgoraWebhookEventDispatcher _dispatcher = dispatcher;

        public async Task ProcessAgoraWebhook(string body, PerformContext context)
        {
            context.WriteLine("[ProcessAgoraWebhook] Process running... Payload: {body}", body);
            var eventType = JsonSerializer
                .Deserialize<JsonElement>(body)
                .GetProperty("eventType")
                .GetInt32();

            switch ((EAgoraEvent)eventType)
            {
                case EAgoraEvent.ChannelCreated:
                    var payload101 = JsonSerializer
                        .Deserialize<AgoraWebhookPayload<Agora101Payload>>(body) ?? 
                            throw new ArgumentNullException(nameof(AgoraWebhookPayload<Agora101Payload>));
                    
                    await _dispatcher.DispatchAsync(payload101);
                    break;
                case EAgoraEvent.ChannelDestroyed:
                    var payload102 = JsonSerializer.Deserialize<AgoraWebhookPayload<Agora102Payload>>(body) ??
                            throw new ArgumentNullException(nameof(AgoraWebhookPayload<Agora102Payload>));

                    await _dispatcher.DispatchAsync(payload102);
                    break;
                case EAgoraEvent.HostJoins:
                    var payload103 = JsonSerializer.Deserialize<AgoraWebhookPayload<Agora103Payload>>(body) ??
                            throw new ArgumentNullException(nameof(AgoraWebhookPayload<Agora103Payload>));

                    await _dispatcher.DispatchAsync(payload103);
                    break;
                case EAgoraEvent.HostLeaves:
                    var payload104 = JsonSerializer.Deserialize<AgoraWebhookPayload<Agora104Payload>>(body) ??
                            throw new ArgumentNullException(nameof(AgoraWebhookPayload<Agora104Payload>));

                    await _dispatcher.DispatchAsync(payload104);
                    break;
                case EAgoraEvent.AudienceJoins:
                    var payload105 = JsonSerializer.Deserialize<AgoraWebhookPayload<Agora105Payload>>(body) ??
                            throw new ArgumentNullException(nameof(AgoraWebhookPayload<Agora105Payload>));

                    await _dispatcher.DispatchAsync(payload105);
                    break;
                case EAgoraEvent.AudienceLeaves:
                    var payload106 = JsonSerializer.Deserialize<AgoraWebhookPayload<Agora106Payload>>(body) ??
                            throw new ArgumentNullException(nameof(AgoraWebhookPayload<Agora106Payload>));

                    await _dispatcher.DispatchAsync(payload106);
                    break;
            }
        }
    }
}