using KwikNesta.Shared.ServiceDTOs.Payment;
using KwikNestaPayment.Infrastructure.Contracts;

namespace KwikNestaPayment.Infrastructure.Services
{
    public class TransferSuccessHandler : IPaystackWebhookHandler
    {
        public string EventType => "transfer.success";

        public async Task HandleAsync(PaystackWebhookDto payload)
        {
            var transferCode = payload.Data.Transfer_code;
            await Task.CompletedTask;
            //await MarkTransferSuccessful(transferCode);
        }
    }
}
