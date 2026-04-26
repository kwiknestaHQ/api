using KwikNesta.Shared.ServiceDTOs.Payment;
using KwikNestaPayment.Infrastructure.Contracts;

namespace KwikNestaPayment.Infrastructure.Services
{
    public class RefundProcessedHandler : IPaystackWebhookHandler
    {
        public string EventType => "refund.processed";

        public async Task HandleAsync(PaystackWebhookDto payload)
        {
            //long refundId = data.id;
            //string status = data.status;

            //var refund = await _refundRepo.GetByRefundId(refundId);
            //if (refund == null) return;

            //// Idempotency check (VERY IMPORTANT)
            //if (refund.Status == EPayoutStatus.Completed)
            //    return;

            //refund.Status = EPayoutStatus.Completed;
            //refund.CompletedAt = DateTime.UtcNow;

            //await _refundRepo.Update(refund);

            // Optional: trigger domain event / notification
        }
    }
}