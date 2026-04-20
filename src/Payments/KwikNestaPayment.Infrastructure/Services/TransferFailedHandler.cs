using KwikNesta.Shared.ServiceDTOs.Payment;
using KwikNestaPayment.Infrastructure.Contracts;

namespace KwikNestaPayment.Infrastructure.Services
{
    public class TransferFailedHandler : IPaystackWebhookHandler
    {
        public string EventType => "transfer.failed";

        public async Task HandleAsync(PaystackWebhookDto payload)
        {
            var transferCode = payload.Data.Transfer_code;
            await Task.CompletedTask;
            //await HandleTransferFailure(transferCode);
        }
    }
}
