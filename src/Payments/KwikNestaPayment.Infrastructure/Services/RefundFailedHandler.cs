using KwikNesta.Shared.Models.Enumerations.Payments;
using KwikNesta.Shared.ServiceDTOs.Payment;
using KwikNestaPayment.Infrastructure.Contracts;
using Microsoft.Extensions.Logging;

namespace KwikNestaPayment.Infrastructure.Services
{
    public class RefundFailedHandler(IPaymentRepositoryManager paymentRepository,
                                PaymentRouter paymentRouter,
                                ILogger<RefundFailedHandler> logger) : IPaystackWebhookHandler
    {
        private readonly IPaymentRepositoryManager _paymentRepository = paymentRepository;
        private readonly PaymentRouter _paymentRouter = paymentRouter;
        private readonly ILogger<RefundFailedHandler> _logger = logger;

        public string EventType => "refund.failed";

        public async Task HandleAsync(PaystackWebhookDto payload)
        {
            _logger.LogInformation("=== [RefundFailedHandler] Received {EventType} event. {payload}", EventType, payload);
            var reference = payload.Data.Reference;

            var refund = await _paymentRepository.Refund
                .FirstOrDefault(x => x.ProviderReference == reference, true);

            if (refund == null)
            {
                _logger.LogError("=== [RefundFailedHandler] Refund record not found with refrence: {reference}", reference);
                return;
            }

            refund.Status = EPayoutStatus.Failed;
            refund.LastUpdatedOn = DateTime.UtcNow;

            await _paymentRepository.SaveAsync();
            _logger.LogInformation("=== [RefundFailedHandler] {EventType} event successfully handled for {reference}.", EventType, reference);
        }
    }
}