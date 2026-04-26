using FluentValidation;
using KwikNesta.Shared.ServiceCommands.Payment;

namespace KwikNestaPayment.Application.Validations
{
    internal class InitiateTransferCommandValidator : AbstractValidator<InitiateTransferCommand>
    {
        public InitiateTransferCommandValidator()
        {
            RuleFor(x => x.Amount).GreaterThan(default(decimal))
                .WithMessage($"Amount must be greater than {default(decimal)}");
            RuleFor(x => x.RecipientCode).NotEmpty()
                .WithMessage("Please provide a valid recipient code");
            RuleFor(x => x.Narration).NotEmpty()
                .WithMessage($"Transfer narration is required");
        }
    }
}