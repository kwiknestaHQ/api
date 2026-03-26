using FluentValidation;
using KwikNesta.Shared.ServiceCommands.Identity;

namespace KwikNestaIdentity.Application.Validations
{
    internal class OtpResendValidator : AbstractValidator<ResendOtpCommand>
    {
        public OtpResendValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("{PropertyName} field is required.")
                .EmailAddress().WithMessage("Please enter a valid email address.");
            RuleFor(x => x.Type)
                .IsInEnum().WithMessage("Invalid OTP type");
        }
    }
}