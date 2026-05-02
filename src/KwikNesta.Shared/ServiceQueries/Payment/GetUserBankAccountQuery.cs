using KwikNesta.Mediator.Cores.Abstractions;
using KwikNesta.Shared.Responses;
using KwikNesta.Shared.ServiceDTOs.Payment;

namespace KwikNesta.Shared.ServiceQueries.Payment
{
    public class GetUserBankAccountQuery : IKNRequest<Response<PayoutAccountDto>>
    {
        public string UserId { get; set; } = default!;
    }
}