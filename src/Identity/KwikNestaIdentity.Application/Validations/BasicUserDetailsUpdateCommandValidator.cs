using FluentValidation;
using KwikNesta.Shared.ServiceCommands.Identity;

namespace KwikNestaIdentity.Application.Validations
{
    internal class BasicUserDetailsUpdateCommandValidator : AbstractValidator<BasicUserDetailsUpdateCommand>
    {
        public BasicUserDetailsUpdateCommandValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("The First Name field is required.");
            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("The First Name field is required.");
            RuleFor(x => x.Gender)
                .IsInEnum().WithMessage("Invalid Gender");
            RuleFor(x => x.LoggedInUserId).NotEmpty()
               .WithMessage("Access denied. User not authenticated");
        }
    }
}