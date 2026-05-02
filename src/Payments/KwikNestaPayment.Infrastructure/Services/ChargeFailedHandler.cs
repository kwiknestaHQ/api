using KwikNesta.Mediator.Cores.Abstractions;
using KwikNesta.Shared.Models.Enumerations.Payments;
using KwikNesta.Shared.ServiceDTOs.Payment;
using KwikNestaPayment.Infrastructure.Contracts;
using Microsoft.Extensions.Logging;

namespace KwikNestaPayment.Infrastructure.Services
{
    public class ChargeFailedHandler(IPaymentRepositoryManager repository, 
                                ILogger<ChargeFailedHandler> logger, 
                                IKNMediator mediator,
                                PaymentRouter paymentRouter) : IPaystackWebhookHandler
    {
        private readonly IPaymentRepositoryManager _repository = repository;
        private readonly ILogger<ChargeFailedHandler> _logger = logger;
        private readonly IKNMediator _mediator = mediator;
        private readonly PaymentRouter _paymentRouter = paymentRouter;

        public string EventType => "charge.failed";

        public async Task HandleAsync(PaystackWebhookDto payload)
        {
            _logger.LogInformation("=== [ChargeFailedHandler] Received {EventType} event. {payload}", EventType, payload);
            var reference = payload.Data.Reference;
            var payment = await _repository.Payment
                .FirstOrDefault(x => x.Reference == reference, true);

            if (payment == null)
            {
                _logger.LogError("Payment not found: {reference}", reference);
                return;
            }

            if (payment.Status == EPaymentStatus.Successful)
                return;

            payment.Status = EPaymentStatus.Failed;
            payment.PaidAt = DateTime.UtcNow;
            await _repository.SaveAsync();

            await _paymentRouter.Route(payment);
            _logger.LogInformation("=== [ChargeFailedHandler] {EventType} event successfully handled for {reference}.", EventType, reference);
        }
    }
}