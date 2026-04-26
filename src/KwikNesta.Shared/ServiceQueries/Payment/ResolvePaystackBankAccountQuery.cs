using KwikNesta.Mediator.Cores.Abstractions;
using KwikNesta.Shared.Responses;
using KwikNesta.Shared.ServiceDTOs.Payment;

namespace KwikNesta.Shared.ServiceQueries.Payment
{
    public class ResolvePaystackBankAccountQuery : IKNRequest<Response<PaystackAccountResolutionResponse>>
    {
        public string AccountNumber { get; set; } = default!;
        public string BankCode { get; set; } = default!;
    }
}