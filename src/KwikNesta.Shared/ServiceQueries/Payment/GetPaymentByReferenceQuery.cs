using KwikNesta.Mediator.Cores.Abstractions;
using KwikNesta.Shared.Responses;
using KwikNesta.Shared.ServiceDTOs.Payment;

namespace KwikNesta.Shared.ServiceQueries.Payment
{
    public class GetPaymentByReferenceQuery : IKNRequest<Response<PaymentDto>>
    {
        public string Reference { get; set; } = default!;
    }
}