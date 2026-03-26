using FluentValidation;
using KwikNesta.Shared.ServiceCommands.Identity;

namespace KwikNestaIdentity.Application.Validations
{
    internal class ForgotPasswordCommandValidator : AbstractValidator<ForgotPasswordCommand>
    {
        public ForgotPasswordCommandValidator()
        {
            RuleFor(x => x.Email).NotEmpty().EmailAddress()
                .WithMessage("Please enter a valid email address");
            RuleFor(x => x.Otp).NotEmpty()
                .WithMessage("Please enter a valid OTP");
            RuleFor(x => x.NewPassword)
                .NotEmpty().WithMessage("New Password field is required.")
                .MinimumLength(8).WithMessage("New Password field must be at least 8 characters.");
            RuleFor(x => x.ConfirmNewPassword)
                .NotEmpty().WithMessage("The Confirm Password field is required.")
                .MinimumLength(8).WithMessage("The Confirm New Password field must be at least 8 characters.");
            RuleFor(x => x)
                .Must(args => ValidationHelper.IsPasswordMatch(args.NewPassword, args.ConfirmNewPassword))
                .WithMessage("Password and Confirm NewPassword must match");
        }
    }
}