using FluentValidation;
using KwikNesta.Shared.Helpers;
using KwikNesta.Shared.ServiceCommands.Payment;

namespace KwikNestaPayment.Application.Validations
{
    internal class CreatePaystackTransferRecipientCommandValidator : AbstractValidator<CreatePaystackTransferRecipientCommand>
    {
        public CreatePaystackTransferRecipientCommandValidator()
        {
            RuleFor(x => x.Currency).NotEmpty().MaximumLength(3)
                .WithMessage("Currency is not valid.");
            RuleFor(x => x.BankCode).NotEmpty()
                .WithMessage("Please provide a valid bank code");
            RuleFor(x => x.BankName).NotEmpty()
                .WithMessage($"Please provide a valid bank name");
            RuleFor(x => x.AccountNumber).NotEmpty()
                .WithMessage($"Please provide a valid account number");
            RuleFor(x => x.AccountType).NotEmpty()
                .WithMessage($"Please provide a valid account type");
        }
    }
}