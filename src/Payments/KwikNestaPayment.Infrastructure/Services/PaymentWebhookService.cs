using Hangfire.Console;
using Hangfire.Server;
using KwikNesta.Shared.ServiceDTOs.Payment;
using KwikNestaPayment.Infrastructure.Contracts;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace KwikNestaPayment.Infrastructure.Services
{
    public class PaymentWebhookService(PaystackWebhookDispatcher dispatcher) : IPaymentWebhookService
    {
        private readonly PaystackWebhookDispatcher _dispatcher = dispatcher;

        public async Task ProcessPaystackWebhook(string body, PerformContext context)
        {
            context.WriteLine("[ProcessPaystackWebhook] Process running...");
            var payload = JsonSerializer.Deserialize<PaystackWebhookDto>(body, new JsonSerializerOptions
            {
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

            if(payload == null)
            {
                context.WriteLine("[ProcessPaystackWebhook] Payload is null");
                return;
            }

            context.WriteLine("[ProcessPaystackWebhook] Dispatching event: {0}", payload.Event);
            await _dispatcher.DispatchAsync(payload);
            context.WriteLine("[ProcessPaystackWebhook] Dispatched event: {0}", payload.Event);
        }
    }
}