using FluentValidation;
using KwikNesta.Shared.Helpers;
using KwikNesta.Shared.ServiceCommands.Property;

namespace KwikNestaProperty.Application.Validations
{
    internal class CreatePropertyCommandValidator : AbstractValidator<CreatePropertyCommand>
    {
        public CreatePropertyCommandValidator()
        {
            RuleFor(x => x.UserContext).Must(ValidationHelper.ValidUserContext)
                .WithMessage("User must be authenticated");

            RuleFor(x => x.Title).NotEmpty().WithMessage("Title is a required field");
            RuleFor(x => x.Price).GreaterThan(default(decimal)).WithMessage($"Price must be greater than {default(decimal)}");
            RuleFor(x => x.AreaSize).GreaterThan(default(double)).WithMessage($"Area Size must be greater than {default(double)}");
            RuleFor(x => x.Type).IsInEnum().WithMessage($"Please select a valid Property Type");
            RuleFor(x => x.Location).Must(loc => loc != null).WithMessage($"Property location is required");
            RuleFor(x => x.Location).Must(loc => loc != null && !string.IsNullOrWhiteSpace(loc.Address))
                .WithMessage($"Address is required");
            RuleFor(x => x.Location).Must(loc => loc != null && !string.IsNullOrWhiteSpace(loc.City))
                .WithMessage($"City is required");
            RuleFor(x => x.Location).Must(loc => loc != null && loc.StateId != Guid.Empty)
                .WithMessage($"Please select a valid state");
            RuleFor(x => x.Location).Must(loc => loc != null && loc.CountryId != Guid.Empty)
                .WithMessage($"Please select a valid country");
            RuleFor(x => x.Location).Must(loc => ValidationHelper.IsValidCoordinates(loc.Latitude, loc.Longitude))
                .WithMessage("Invalid coordinates");
            RuleFor(x => x.ListingType).IsInEnum()
                .WithMessage("Please select a valid listing type");
            RuleFor(x => x.PriceFrequency).IsInEnum()
                .WithMessage("Please select a valid price frequency");
            RuleFor(x => x).Must(args => ValidationHelper.ValidForListingType(args.ListingType, args.PriceFrequency))
                .WithMessage("Invalid combination of listing type and price frequency.");
        }
    }
}