using FluentValidation;
using KwikNesta.Shared.ServiceCommands.Infra;

namespace KwikNestaInfra.Application.Validators
{
    internal class CreateAgoraTokenCommandValidator : AbstractValidator<CreateAgoraTokenCommand>
    {
        public CreateAgoraTokenCommandValidator()
        {
            RuleFor(x => x.UserId)
             .NotEmpty().WithMessage("UserId field is required.");
            RuleFor(x => x.Channel)
              .NotEmpty().WithMessage("Channel field is required.");
            RuleFor(x => x.Token)
               .NotEmpty().WithMessage("Token field is required.");
            RuleFor(x => x.Expires)
                .Must(date => date > DateTime.UtcNow)
                .WithMessage("Invalid expiry date");
        }
    }
}
