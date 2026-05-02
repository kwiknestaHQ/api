using KwikNesta.Mediator.Cores.Abstractions;
using KwikNesta.Shared.Responses;
using KwikNesta.Shared.ServiceDTOs.Payment;
using KwikNesta.Shared.ServiceQueries.Payment;
using KwikNestaPayment.Infrastructure;
using Microsoft.AspNetCore.Http;

namespace KwikNestaPayment.Application.Handlers
{
    public class GetUserBankAccountsQueryHandler(IPaymentRepositoryManager repository)
                : IKNRequestHandler<GetUserBankAccountQuery, Response<PayoutAccountDto>>
    {
        private readonly IPaymentRepositoryManager _repository = repository;

        public async Task<Response<PayoutAccountDto>> HandleAsync(GetUserBankAccountQuery request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.UserId))
            {
                return Response<PayoutAccountDto>.Fail(PaymentResponses.InvalidRequest,
                    StatusCodes.Status400BadRequest);
            }

            var account = await _repository.PayoutAccount
                .FirstOrDefault(acc => acc.UserId == request.UserId &&
                    !acc.IsDeprecated && acc.IsActive);

            if(account == null)
            {
                return Response<PayoutAccountDto>.Fail(string.Format(PaymentResponses.RecordNotFound, "Bank Account"),
                    StatusCodes.Status404NotFound);
            }

            return Response<PayoutAccountDto>.Ok(account.Map());
        }
    }
}