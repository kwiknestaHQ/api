using FluentValidation;
using KwikNesta.Shared.ServiceNotifications.Infra;

namespace KwikNestaInfra.Application.Validators
{
    internal class CreateAuditNotificationValidator : AbstractValidator<CreateAuditNotification>
    {
        public CreateAuditNotificationValidator()
        {
            RuleFor(x => x.UserId)
              .NotEmpty().WithMessage("Id field is required.");
            RuleFor(x => x.DomainId)
              .NotEmpty().WithMessage("Id field is required.");
            RuleFor(x => x.UserName)
               .NotEmpty().WithMessage("User name field is required.")
               .EmailAddress().WithMessage("Please enter a valid email address.");
            RuleFor(x => x.Action)
                .IsInEnum().WithMessage("Invalid Audit Action");
            RuleFor(x => x.Domain)
               .IsInEnum().WithMessage("Invalid Audit Domain");
        }
    }
}