using FluentValidation;
using KwikNesta.Shared.ServiceCommands.Payment;

namespace KwikNestaPayment.Application.Validations
{
    internal class CreateSettlementCommandValidator : AbstractValidator<CreateSettlementCommand>
    {
        public CreateSettlementCommandValidator()
        {
            RuleFor(x => x.UserId).NotEmpty()
                .WithMessage("User Id is required");

            RuleFor(x => x.Amount).Must(amt => amt <= default(decimal))
                .WithMessage("Amount must be greater than 0.00");

            RuleFor(x => x.ReferenceId).Must(ri => ri != Guid.Empty)
                .WithMessage("Please provide a valid Reference Id");

            RuleFor(x => x.Purpose).IsInEnum()
                .WithMessage("Please provide a valid Settlement Purpose");

            RuleFor(x => x.SettlementType).IsInEnum()
                .WithMessage("Please provide a valid Settlement Type");
        }
    }
}