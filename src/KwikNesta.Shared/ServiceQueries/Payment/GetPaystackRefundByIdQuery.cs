using KwikNesta.Mediator.Cores.Abstractions;
using KwikNesta.Shared.Responses;
using KwikNesta.Shared.ServiceDTOs.Payment;

namespace KwikNesta.Shared.ServiceQueries.Payment
{
    public class GetPaystackRefundByIdQuery : IKNRequest<Response<PaystackRefundQueryResponseData>>
    {
        public string RefundId { get; set; } = default!;
    }
}