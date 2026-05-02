using KwikNesta.Mediator.Cores.Abstractions;
using KwikNesta.Shared.Responses;
using KwikNesta.Shared.ServiceDTOs.Payment;

namespace KwikNesta.Shared.ServiceQueries.Payment
{
    public class GetPaymentByReferenceIdQuery : IKNRequest<Response<PaymentDto>>
    {
        public Guid ReferenceId { get; set; }
    }
}