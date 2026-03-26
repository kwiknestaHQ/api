using FluentValidation;
using KwikNesta.Shared.ServiceCommands.Identity;

namespace KwikNestaIdentity.Application.Validations
{
    internal class AccountRestoreCommandValidator : AbstractValidator<AccountRestoreCommand>
    {
        public AccountRestoreCommandValidator()
        {
            RuleFor(x => x.UserId).NotEmpty()
                .WithMessage("User Id to be suspended is required");
            RuleFor(x => x.LoggedInUserId).NotEmpty()
                .WithMessage("Access denied. User not authenticated");
        }
    }
}