using FluentValidation;
using KwikNesta.Shared.Helpers;
using KwikNesta.Shared.ServiceCommands.Property;

namespace KwikNestaProperty.Application.Validations
{
    internal class UpdatePropertyFeaturesCommandValidator : AbstractValidator<UpdatePropertyFeaturesCommand>
    {
        public UpdatePropertyFeaturesCommandValidator()
        {
            RuleFor(x => x.UserContext).Must(ValidationHelper.ValidUserContext)
                .WithMessage("User must be authenticated");

            RuleFor(x => x.PropertyId).Must(id => id != Guid.Empty).WithMessage("Property Id is required");
            RuleFor(x => x.FeatureIds).Must(fi => fi.All(f => f != Guid.Empty)).WithMessage("Selected Feature Ids can not be empty");
            RuleFor(x => x.CustomFeatures).Must(fi => fi.All(f => !string.IsNullOrWhiteSpace(f))).WithMessage("Custom Feature can not be empty");
        }
    }
}