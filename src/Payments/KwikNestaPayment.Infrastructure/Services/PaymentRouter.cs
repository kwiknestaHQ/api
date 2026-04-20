using KwikNesta.Mediator.Cores.Abstractions;
using KwikNesta.Shared.Models.Enumerations.Payments;
using KwikNesta.Shared.ServiceCommands.Property;
using KwikNestaPayment.Domain.Entities;

namespace KwikNestaPayment.Infrastructure.Services
{
    public class PaymentRouter(IKNMediator mediator)
    {
        private readonly IKNMediator _mediator = mediator;

        public async Task Route(KNPayment payment)
        {
            switch (payment.Purpose)
            {
                case EPaymentPurpose.Viewing:
                    await HandleInspection(payment);
                    break;

                case EPaymentPurpose.Rent:
                    await HandleRent(payment);
                    break;
            }
        }

        private async Task HandleInspection(KNPayment payment)
        {
            await _mediator.PublishAsync(new FinalizeViewRequestNotification
            {
                Reference = payment.Reference
            });
        }

        private async Task HandleRent(KNPayment payment)
        {
            await Task.CompletedTask;
        }
    }
}
