using KwikNesta.Shared.ServiceDTOs.Payment;
using KwikNestaPayment.Infrastructure.Contracts;
using Microsoft.Extensions.Logging;

namespace KwikNestaPayment.Infrastructure.Services
{
    public class PaystackWebhookDispatcher(IEnumerable<IPaystackWebhookHandler> handlers, 
                                        ILogger<PaystackWebhookDispatcher> logger)
    {
        private readonly Dictionary<string, IPaystackWebhookHandler> _handlers = handlers.ToDictionary(h => h.EventType, h => h);
        private readonly ILogger<PaystackWebhookDispatcher> _logger = logger;

        public async Task DispatchAsync(PaystackWebhookDto payload)
        {
            if (!_handlers.TryGetValue(payload.Event, out var handler))
            {
                _logger.LogWarning("[DispatchAsync] Unknown Event: {Event}", payload.Event);
                return;
            }

            await handler.HandleAsync(payload);
        }
    }
}