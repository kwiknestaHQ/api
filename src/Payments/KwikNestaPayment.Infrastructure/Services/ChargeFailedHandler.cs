using KwikNesta.Shared.ServiceDTOs.Payment;
using KwikNestaPayment.Infrastructure.Contracts;

namespace KwikNestaPayment.Infrastructure.Services
{
    public class ChargeFailedHandler : IPaystackWebhookHandler
    {
        public string EventType => "charge.failed";

        public async Task HandleAsync(PaystackWebhookDto payload)
        {
            var reference = payload.Data.Reference;
            await Task.CompletedTask;
            //await MarkPaymentFailed(reference);
        }
    }
}