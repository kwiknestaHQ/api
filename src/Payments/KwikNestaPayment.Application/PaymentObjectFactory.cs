using KwikNesta.Shared.ServiceCommands.Payment;
using KwikNesta.Shared.ServiceDTOs.Payment;
using KwikNestaPayment.Domain.Entities;

namespace KwikNestaPayment.Application
{
    internal static class PaymentObjectFactory
    {
        public static KNPayment Map(this CreatePaymentCommand command)
        {
            return new KNPayment
            {
                Amount = command.Amount,
                NetAmount = command.NetAmount,
                PlatformFee = command.PlatformFee,
                Purpose = command.Purpose,
                Reference = command.Reference,
                ReferenceId = command.ReferenceId
            };
        }

        public static PaymentDto Map(this KNPayment payment)
        {
            return new PaymentDto
            {
                Id = payment.Id,
                Reference = payment.Reference,
                Amount = payment.Amount,
                NetAmount = payment.NetAmount,
                PaidAt = payment.PaidAt,
                CreatedOn = payment.CreatedOn,
                Currency = payment.Currency,
                PlatformFee = payment.PlatformFee,
                Purpose = payment.Purpose,
                ReferenceId = payment.ReferenceId,
                Status = payment.Status
            };
        }
    }
}