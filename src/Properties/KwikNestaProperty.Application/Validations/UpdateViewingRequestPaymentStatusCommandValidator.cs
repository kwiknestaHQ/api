using FluentValidation;
using KwikNesta.Shared.ServiceCommands.Property;

namespace KwikNestaProperty.Application.Validations
{
    internal class UpdateViewingRequestPaymentStatusCommandValidator : AbstractValidator<UpdateViewingRequestPaymentStatusCommand>
    {
        public UpdateViewingRequestPaymentStatusCommandValidator()
        {
            RuleFor(x => x.Id).Must(id => id != Guid.Empty)
                .WithMessage("Please provide a valid Id");
            RuleFor(x => x.NewStatus).IsInEnum()
                .WithMessage("Please provide a valid Status");
        }
    }
}
