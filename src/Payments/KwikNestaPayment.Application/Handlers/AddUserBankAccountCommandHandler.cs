using KwikNesta.Mediator.Cores.Abstractions;
using KwikNesta.Shared.Constants;
using KwikNesta.Shared.Implementations;
using KwikNesta.Shared.Models.Enumerations.Infra;
using KwikNesta.Shared.Responses;
using KwikNesta.Shared.ServiceCommands.Payment;
using KwikNestaPayment.Application.Validations;
using KwikNestaPayment.Domain.Entities;
using KwikNestaPayment.Infrastructure;
using Microsoft.AspNetCore.Http;

namespace KwikNestaPayment.Application.Handlers
{
    public class AddUserBankAccountCommandHandler(IPaymentRepositoryManager repository,
                                    IKNMediator mediator) 
        : IKNRequestHandler<AddUserBankAccountCommand, Response<string>>
    {
        private readonly IPaymentRepositoryManager _repository = repository;
        private readonly IKNMediator _mediator = mediator;

        public async Task<Response<string>> HandleAsync(AddUserBankAccountCommand request, CancellationToken cancellationToken)
        {
            var validator = new AddUserBankAccountCommandValidator().Validate(request);
            if (!validator.IsValid)
            {
                return Response<string>.Fail(validator.Errors.FirstOrDefault()?.ErrorMessage ?? 
                                    PaymentResponses.InvalidRequest,
                                    StatusCodes.Status400BadRequest);
            }

            var existingAccount = await _repository.PayoutAccount
                .FirstOrDefault(acc => acc.UserId == request.Context.Id);
            if(existingAccount != null)
            {
                return Response<string>.Fail(PaymentResponses.ExistingUserAccount, 
                    StatusCodes.Status409Conflict);
            }

            var recipientResponse = await _mediator.SendAsync(new CreatePaystackTransferRecipientCommand
            {
                BankCode = request.BankCode,
                AccountNumber = request.AccountNumber,
                BankName = request.BankName,
                AccountType = request.Type,
                Currency = request.Currency
            });

            if (!recipientResponse.Success)
            {
                return Response<string>.Fail(recipientResponse.Message, recipientResponse.StatusCode);
            }

            var data = recipientResponse.Data;
            if(data == null || data.Details == null)
            {
                return Response<string>.Fail(PaymentResponses.InvalidRequest, 
                    StatusCodes.Status400BadRequest);
            }

            var bankAccount = data.Details.Map(request.Context.Id, data.RecipientCode);

            await _repository.PayoutAccount.AddAsync(bankAccount);
            await _repository.SaveAsync();

            AppAudit.Write(request.Context.Id,
                request.Context.Email,
                EAuditAction.AddedUserBankAccount,
                EAuditDomain.Payment,
                bankAccount.Id.ToString(),
                request.Context.IpAddress);
            return Response<string>.Ok(PaymentResponses.AccountNumberAdded);
        }
    }
}