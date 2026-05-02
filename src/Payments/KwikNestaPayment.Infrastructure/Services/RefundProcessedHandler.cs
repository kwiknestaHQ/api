using KwikNesta.Mediator.Cores.Abstractions;
using KwikNesta.Shared.Models.Enumerations.Payments;
using KwikNesta.Shared.ServiceDTOs.Payment;
using KwikNestaPayment.Infrastructure.Contracts;
using Microsoft.Extensions.Logging;

namespace KwikNestaPayment.Infrastructure.Services
{
    public class RefundProcessedHandler(IKNMediator mediator, 
                                    IPaymentRepositoryManager paymentRepository,
                                    PaymentRouter paymentRouter,
                                    ILogger<RefundProcessedHandler> logger) 
        : IPaystackWebhookHandler
    {
        private readonly IKNMediator _mediator = mediator;
        private readonly IPaymentRepositoryManager _paymentRepository = paymentRepository;
        private readonly PaymentRouter _paymentRouter = paymentRouter;
        private readonly ILogger<RefundProcessedHandler> _logger = logger;

        public string EventType => "refund.processed";

        public async Task HandleAsync(PaystackWebhookDto payload)
        {
            _logger.LogInformation("=== [RefundProcessedHandler] Received {EventType} event. {payload}", EventType, payload);
            var reference = payload.Data.Reference;

            var refund = await _paymentRepository.Refund
                .FirstOrDefault(x => x.ProviderReference == reference, true);

            if (refund == null)
            {
                _logger.LogError("=== [RefundProcessedHandler] Refund record not found with refrence: {reference}", reference);
                return;
            }

            refund.Status = EPayoutStatus.Success;
            refund.LastUpdatedOn = DateTime.UtcNow;

            var payment = await _paymentRepository.Payment
                .FirstOrDefault(p => p.Reference == refund.Reference);

            if(payment == null)
            {
                _logger.LogError("=== [RefundProcessedHandler] Payment record not found with refrence: {Reference}", refund.Reference);
                return;
            }

            payment.Status = EPaymentStatus.Refunded;
            payment.LastUpdatedOn = DateTime.UtcNow;

            await _paymentRepository.SaveAsync();
            await _paymentRouter.Route(payment);
            _logger.LogInformation("=== [RefundProcessedHandler] {EventType} with {reference} successfully processed", EventType, reference);
        }
    }
}