using FluentValidation;
using KwikNesta.Shared.ServiceCommands.Payment;

namespace KwikNestaPayment.Application.Validations
{
    internal class InitiatePaystackRefundCommandValidator
        : AbstractValidator<InitiatePaystackRefundCommand>
    {
        public InitiatePaystackRefundCommandValidator()
        {
            RuleFor(x => x.PaymentReference).NotEmpty()
                .WithMessage("Payment Reference is required.");
            RuleFor(x => x.Amount).Must(amt => amt > default(decimal))
                .WithMessage($"The amount must be greater than {default(decimal)}");
        }
    }
}