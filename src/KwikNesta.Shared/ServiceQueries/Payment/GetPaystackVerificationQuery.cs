using KwikNesta.Mediator.Cores.Abstractions;
using KwikNesta.Shared.Responses;
using KwikNesta.Shared.ServiceDTOs.Payment;

namespace KwikNesta.Shared.ServiceQueries.Payment
{
    public class GetPaystackVerificationQuery : IKNRequest<Response<PaystackVerifyResponse>>
    {
        public string Reference { get; set; } = default!;
    }
}