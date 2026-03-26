using FluentValidation;
using KwikNesta.Shared.ServiceCommands.Identity;

namespace KwikNestaIdentity.Application.Validations
{
    internal class RegistrationCommandValidator : AbstractValidator<RegistrationCommand>
    {
        public RegistrationCommandValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("{PropertyName} field is required.")
                .EmailAddress().WithMessage("Please enter a valid email address.");
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("The First Name field is required.");
            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("The First Name field is required.");
            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage("The Phone Number field is required.");
            RuleFor(x => x)
                .Must(args => ValidationHelper.IsValidE164(args.PhoneNumber))
                .WithMessage("Phone Number not in the correct format");
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("{PropertyName} field is required.")
                .MinimumLength(8).WithMessage("{PropertyName} field must be at least 8 characters.");
            RuleFor(x => x.ConfirmPassword)
                .NotEmpty().WithMessage("The Confirm Password field is required.")
                .MinimumLength(8).WithMessage("{PropertyName} field must be at least 8 characters.");
            RuleFor(x => x)
                .Must(args => ValidationHelper.IsPasswordMatch(args.Password, args.ConfirmPassword))
                .WithMessage("Password and Confirm NewPassword must match");
            RuleFor(x => x.Role)
                .IsInEnum().WithMessage("Invalid role type");
            RuleFor(x => x.Gender)
                .IsInEnum().WithMessage("Invalid Gender");
        }
    }
}