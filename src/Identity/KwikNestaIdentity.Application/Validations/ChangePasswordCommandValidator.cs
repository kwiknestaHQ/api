using FluentValidation;
using KwikNesta.Shared.ServiceCommands.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KwikNestaIdentity.Application.Validations
{
    internal class ChangePasswordCommandValidator : AbstractValidator<ChangePasswordCommand>
    {
        public ChangePasswordCommandValidator()
        {
            RuleFor(x => x.CurrentPassword)
                .NotEmpty().WithMessage("User Name field is required.")
                .EmailAddress().WithMessage("Please enter a valid email address.");
            RuleFor(x => x.NewPassword)
               .NotEmpty().WithMessage("New Password field is required.")
               .MinimumLength(8).WithMessage("New Password field must be at least 8 characters.");
            RuleFor(x => x)
                .Must(args => ValidationHelper.IsPasswordMatch(args.NewPassword, args.ConfirmNewPassword))
                .WithMessage("New Password and Confirm New Password must match");
            RuleFor(x => x.UserId)
                .Must(userId => ValidationHelper.ValidUserId(userId))
                .WithMessage("Invalid userId. User must be logged in");
        }
    }
}
