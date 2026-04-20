using FluentValidation;
using KwikNesta.Shared.Helpers;
using KwikNesta.Shared.ServiceCommands.Payment;

namespace KwikNestaPayment.Application.Validations
{
    internal class CreatePaymentCommandValidator : AbstractValidator<CreatePaymentCommand>
    {
        public CreatePaymentCommandValidator()
        {
            RuleFor(x => x.Context).Must(ValidationHelper.ValidUserContext)
                .WithMessage("User must be authenticated");

            RuleFor(x => x.Reference).NotEmpty().WithMessage("Reference is a required field");
            RuleFor(x => x.Amount).GreaterThan(default(decimal)).WithMessage($"Amount must be greater than {default(decimal)}");
            RuleFor(x => x.NetAmount).GreaterThan(default(decimal)).WithMessage($"Net Amount Size must be greater than {default(double)}");
            RuleFor(x => x.Purpose).IsInEnum().WithMessage($"Please select a valid Payment Purpose");
            RuleFor(x => x.ReferenceId).Must(rf => rf != Guid.Empty).WithMessage($"Reference Id is required");
        }
    }
}