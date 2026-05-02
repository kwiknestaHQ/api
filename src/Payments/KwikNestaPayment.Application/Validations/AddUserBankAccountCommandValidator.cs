using FluentValidation;
using KwikNesta.Shared.Helpers;
using KwikNesta.Shared.ServiceCommands.Payment;

namespace KwikNestaPayment.Application.Validations
{
    internal class AddUserBankAccountCommandValidator : AbstractValidator<AddUserBankAccountCommand>
    {
        public AddUserBankAccountCommandValidator()
        {
            RuleFor(x => x.Context).Must(ValidationHelper.ValidUserContext)
               .WithMessage("User must be authenticated");

            RuleFor(x => x.BankCode).NotEmpty().WithMessage("Bank Code is a required field.");
            RuleFor(x => x.BankName).NotEmpty().WithMessage($"Bank Name is a required field.");
            RuleFor(x => x.AccountNumber).NotEmpty().WithMessage($"Account Number is a required field.");
            RuleFor(x => x.Type).NotEmpty().WithMessage($"Account Type is a required field.");
            RuleFor(x => x.Currency).NotEmpty().WithMessage($"Currency is a required field.");
        }
    }
}