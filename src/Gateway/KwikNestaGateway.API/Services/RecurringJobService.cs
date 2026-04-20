using Hangfire.Console;
using Hangfire.Server;
using KwikNesta.Mediator.Cores.Abstractions;
using KwikNesta.Shared.Extensions;
using KwikNesta.Shared.Models.Enumerations.Payments;
using KwikNesta.Shared.ServiceQueries.Payment;
using KwikNestaPayment.Infrastructure;
using KwikNestaPayment.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;

namespace KwikNestaGateway.API.Services
{
    public class RecurringJobService(IPaymentRepositoryManager paymentRepository,
                                IKNMediator mediator,
                                PaymentRouter router) : IRecurringJobService
    {
        private readonly IPaymentRepositoryManager _paymentRepository = paymentRepository;
        private readonly IKNMediator _mediator = mediator;
        private readonly PaymentRouter _router = router;

        public async Task RunPaymentVerifications(PerformContext context)
        {
            context.WriteLine("===[RunPaymentVerifications] Running job===");

            var payments = await _paymentRepository.Payment
                .Get(p => !p.IsDeprecated && 
                    !p.PaidAt.HasValue && 
                    (p.Status == EPaymentStatus.Pending || p.Status == EPaymentStatus.Failed))
                .OrderBy(p => p.CreatedOn)
                .Take(20)
                .ToListAsync();

            foreach (var payment in payments.WithProgress(context))
            {
                var verify = await _mediator.SendAsync(new GetPaystackVerificationQuery
                {
                    Reference = payment.Reference
                });

                if (!verify.Success)
                {
                    context.WriteLine($"===[RunPaymentVerifications] Payment verification failed for: {payment.Reference}===");
                    return;
                }

                var verification = verify.Data;
                if (!verification.Status || verification.Data.Status != "success")
                {
                    context.WriteLine($"===[RunPaymentVerifications] Payment verification failed for: {payment.Reference}===");
                    return;
                }

                if (verification.Data.Amount != payment.NetAmount.ToMinorUnits())
                {
                    context.WriteLine($"===[RunPaymentVerifications] Amount mismatch for: {payment.Reference}===");
                    return;
                }

                if (verification.Data.Currency != payment.Currency)
                {
                    context.WriteLine($"===[RunPaymentVerifications] Currency mismatch for: {payment.Reference}===");
                    return;
                }

                payment.Status = EPaymentStatus.Successful;
                payment.PaidAt = DateTime.UtcNow;
                await _paymentRepository.SaveAsync();

                await _router.Route(payment);
            }
        }
    }
}