using KwikNesta.Mediator.Cores.Abstractions;
using KwikNesta.Shared.Models.Enumerations.Payments;
using KwikNesta.Shared.Models.Enumerations.Property;
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
            switch (payment.Status)
            {
                case EPaymentStatus.Pending:
                    break;
                case EPaymentStatus.Successful:
                    await _mediator.PublishAsync(new FinalizeViewRequestNotification
                    {
                        Reference = payment.Reference
                    });
                    break;
                case EPaymentStatus.Failed:
                    await _mediator.SendAsync(new UpdateViewingRequestPaymentStatusCommand
                    {
                        Id = payment.ReferenceId,
                        NewStatus = EViewingPaymentStatus.Failed
                    });
                    break;
                case EPaymentStatus.Refunded:
                    await _mediator.SendAsync(new UpdateViewingRequestPaymentStatusCommand
                    {
                        Id = payment.ReferenceId,
                        NewStatus = EViewingPaymentStatus.Refunded
                    });
                    break;
            }
        }

        private async Task HandleRent(KNPayment payment)
        {
            await Task.CompletedTask;
        }
    }
}
