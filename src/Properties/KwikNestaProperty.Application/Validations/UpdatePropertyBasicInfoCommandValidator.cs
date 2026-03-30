using FluentValidation;
using KwikNesta.Shared.Helpers;
using KwikNesta.Shared.ServiceCommands.Property;

namespace KwikNestaProperty.Application.Validations
{
    internal class UpdatePropertyBasicInfoCommandValidator : AbstractValidator<UpdatePropertyBasicInfoCommand>
    {
        public UpdatePropertyBasicInfoCommandValidator()
        {
            RuleFor(x => x.UserContext).Must(ValidationHelper.ValidUserContext)
               .WithMessage("User must be authenticated");

            RuleFor(x => x.Title).NotEmpty().WithMessage("Title is a required field");
            RuleFor(x => x.Price).GreaterThan(default(decimal)).WithMessage($"Price must be greater than {default(decimal)}");
            RuleFor(x => x.AreaSize).GreaterThan(default(double)).WithMessage($"Area Size must be greater than {default(double)}");
            RuleFor(x => x.Type).IsInEnum().WithMessage($"Please select a valid Property Type");
            RuleFor(x => x.ListingType).IsInEnum()
                .WithMessage("Please select a valid listing type");
            RuleFor(x => x.PriceFrequency).IsInEnum()
                .WithMessage("Please select a valid price frequency");
            RuleFor(x => x).Must(args => ValidationHelper.ValidForListingType(args.ListingType, args.PriceFrequency))
               .WithMessage("Invalid combination of listing type and price frequency.");
        }
    }
}