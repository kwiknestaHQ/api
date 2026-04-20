using FluentValidation;
using KwikNesta.Shared.Helpers;
using KwikNesta.Shared.ServiceCommands.Payment;

namespace KwikNestaPayment.Application.Validations
{
    internal class PaystackChargeCommandValidator : AbstractValidator<PaystackChargeCommand>
    {
        public PaystackChargeCommandValidator()
        {
            RuleFor(x => x.Context).Must(ValidationHelper.ValidUserContext)
                .WithMessage("User must be authenticated");

            RuleFor(x => x.Reference).NotEmpty().WithMessage("Reference is a required field");
            RuleFor(x => x.Amount).GreaterThan(default(decimal)).WithMessage($"Amount must be greater than {default(decimal)}");
            RuleFor(x => x.Email).EmailAddress().WithMessage($"Please provide a valid email address");
        }
    }
}