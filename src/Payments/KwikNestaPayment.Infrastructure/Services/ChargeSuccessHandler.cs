using KwikNesta.Mediator.Cores.Abstractions;
using KwikNesta.Shared.Extensions;
using KwikNesta.Shared.Models.Enumerations.Payments;
using KwikNesta.Shared.ServiceDTOs.Payment;
using KwikNesta.Shared.ServiceQueries.Payment;
using KwikNestaPayment.Infrastructure.Contracts;
using Microsoft.Extensions.Logging;

namespace KwikNestaPayment.Infrastructure.Services
{
    public class ChargeSuccessHandler(IPaymentRepositoryManager repository, 
                                    PaymentRouter paymentRouter,
                                    ILogger<ChargeSuccessHandler> logger,
                                    IKNMediator mediator) : IPaystackWebhookHandler
    {
        private readonly IPaymentRepositoryManager _repository = repository;
        private readonly PaymentRouter _paymentRouter = paymentRouter;
        private readonly ILogger<ChargeSuccessHandler> _logger = logger;
        private readonly IKNMediator _mediator = mediator;

        public string EventType => "charge.success";

        public async Task HandleAsync(PaystackWebhookDto payload)
        {
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

            payment.Status = EPaymentStatus.Successful;
            payment.PaidAt = DateTime.UtcNow;
            await _repository.SaveAsync();

            var verify = await _mediator.SendAsync(new GetPaystackVerificationQuery
            {
                Reference = reference
            });

            if (!verify.Success)
            {
                _logger.LogWarning("Verification API failed for {reference}", reference);
            }
            else
            {
                var v = verify.Data;

                if (v.Data.Amount != payment.NetAmount.ToMinorUnits())
                {
                    _logger.LogWarning("Amount mismatch for {reference}", reference);
                }

                if (v.Data.Currency != payment.Currency)
                {
                    _logger.LogWarning("Currency mismatch for {reference}", reference);
                }

                if (v.Data.Status != "success")
                {
                    _logger.LogWarning(
                        "Verify says {status} but webhook says success for {reference}",
                        v.Data.Status,
                        reference
                    );
                }
            }

            await _paymentRouter.Route(payment);
        }
    }
}