using KwikNesta.Shared.ServiceDTOs.Payment;
using KwikNestaPayment.Infrastructure.Contracts;

namespace KwikNestaPayment.Infrastructure.Services
{
    public class RefundFailedHandler : IPaystackWebhookHandler
    {
        public string EventType => "refund.failed";

        public async Task HandleAsync(PaystackWebhookDto payload)
        {
            //long refundId = data.id;

            //var refund = await _refundRepo.GetByRefundId(refundId);
            //if (refund == null) return;

            //if (refund.Status == EPayoutStatus.Failed)
            //    return;

            //refund.Status = EPayoutStatus.Failed;
            //refund.CompletedAt = DateTime.UtcNow;

            //await _refundRepo.Update(refund);

            // Optional: alert admins / retry logic
        }
    }
}