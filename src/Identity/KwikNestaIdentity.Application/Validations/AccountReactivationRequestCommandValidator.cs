using FluentValidation;
using KwikNesta.Shared.ServiceCommands.Identity;

namespace KwikNestaIdentity.Application.Validations
{
    internal class AccountReactivationRequestCommandValidator : AbstractValidator<AccountReactivationRequestCommand>
    {
        public AccountReactivationRequestCommandValidator()
        {
            RuleFor(x => x.Email).NotEmpty()
                .EmailAddress()
                .WithMessage("Plese enter a valid email address.");
        }
    }
}
