using Hangfire.Console;
using Hangfire.Server;
using KwikNesta.Shared.Extensions;
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
            var root = JsonSerializer.Deserialize<JsonElement>(body);
            var eventType = root.GetProperty("eventType").GetInt32();

            switch ((EAgoraEvent)eventType)
            {
                case EAgoraEvent.ChannelCreated:
                    await _dispatcher.DispatchAsync(
                        root.DeserializeObject<AgoraWebhookPayload<Agora101Payload>>());
                    break;
                case EAgoraEvent.ChannelDestroyed:
                    await _dispatcher.DispatchAsync(
                        root.DeserializeObject<AgoraWebhookPayload<Agora102Payload>>());
                    break;
                case EAgoraEvent.HostJoins:
                    await _dispatcher.DispatchAsync(
                        root.DeserializeObject<AgoraWebhookPayload<Agora103Payload>>());
                    break;
                case EAgoraEvent.HostLeaves:
                    await _dispatcher.DispatchAsync(
                        root.DeserializeObject<AgoraWebhookPayload<Agora104Payload>>());
                    break;
                case EAgoraEvent.AudienceJoins:
                    await _dispatcher.DispatchAsync(
                        root.DeserializeObject<AgoraWebhookPayload<Agora105Payload>>());
                    break;
                case EAgoraEvent.AudienceLeaves:
                    await _dispatcher.DispatchAsync(
                        root.DeserializeObject<AgoraWebhookPayload<Agora106Payload>>());
                    break;
            }
        }
    }
}