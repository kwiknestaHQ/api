using FluentValidation;
using KwikNesta.Shared.Helpers;
using KwikNesta.Shared.ServiceCommands.Property;

namespace KwikNestaProperty.Application.Validations
{
    internal class UpdatePropertyLocationCommandValidator : AbstractValidator<UpdatePropertyLocationCommand>
    {
        public UpdatePropertyLocationCommandValidator()
        {
            RuleFor(x => x.UserContext).Must(ValidationHelper.ValidUserContext)
               .WithMessage("User must be authenticated");
            RuleFor(x => x.City).NotEmpty()
                .WithMessage($"City is required");
            RuleFor(x => x.StateId).Must(state => state != Guid.Empty)
                .WithMessage($"Please select a valid state");
            RuleFor(x => x.CountryId).Must(country => country != Guid.Empty)
                .WithMessage($"Please select a valid country");
            RuleFor(x => x).Must(args => ValidationHelper.IsValidCoordinates(args.Latitude, args.Longitude))
                .WithMessage("Invalid coordinates");
        }
    }
}