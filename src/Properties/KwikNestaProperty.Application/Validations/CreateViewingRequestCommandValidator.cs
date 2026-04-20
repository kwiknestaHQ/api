using FluentValidation;
using KwikNesta.Shared.Helpers;
using KwikNesta.Shared.ServiceCommands.Property;

namespace KwikNestaProperty.Application.Validations
{
    internal class CreateViewingRequestCommandValidator : AbstractValidator<CreateViewingRequestCommand>
    {
        public CreateViewingRequestCommandValidator()
        {
            RuleFor(x => x.Context).Must(ValidationHelper.ValidUserContext)
                .WithMessage("User must be authenticated");
            RuleFor(x => x.Type).IsInEnum().WithMessage($"Please select a valid instection type");
            RuleFor(x => x.PropertyId).Must(id => id != Guid.Empty).WithMessage($"Property Id is required");
            RuleFor(x => x.ScheduledDate).Must(date => date > DateTime.UtcNow)
                .WithMessage($"Scheduled Date must be later than the current date/time");
        }
    }
}