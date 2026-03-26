using FluentValidation;
using KwikNesta.Shared.ServiceCommands.Identity;

namespace KwikNestaIdentity.Application.Validations
{
    internal class AccountReactivationCommandValidator : AbstractValidator<AccountReactivationCommand>
    {
        public AccountReactivationCommandValidator()
        {
            RuleFor(x => x.Email).NotEmpty().EmailAddress()
                .WithMessage("Please enter a valid email address");
            RuleFor(x => x.Otp).NotEmpty()
                .WithMessage("OTP is required");
        }
    }
}