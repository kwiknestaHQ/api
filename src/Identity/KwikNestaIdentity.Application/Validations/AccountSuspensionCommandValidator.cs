using FluentValidation;
using KwikNesta.Shared.ServiceCommands.Identity;

namespace KwikNestaIdentity.Application.Validations
{
    internal class AccountSuspensionCommandValidator : AbstractValidator<AccountSuspensionCommand>
    {
        public AccountSuspensionCommandValidator()
        {
            RuleFor(x => x.UserId).NotEmpty()
                .WithMessage("User Id to be suspended is required");
            RuleFor(x => x.LoggedInUserId).NotEmpty()
                .WithMessage("Access denied. User not authenticated");
            RuleFor(x => x.Reason).IsInEnum()
                .WithMessage("Plese select a valid reason");
            RuleFor(x => x)
                .Must(args => ValidationHelper.ValidSuspensionReason(args.Reason, args.OtherReason))
                .WithMessage("Reason description is required if 'Other' is selected");
        }
    }
}