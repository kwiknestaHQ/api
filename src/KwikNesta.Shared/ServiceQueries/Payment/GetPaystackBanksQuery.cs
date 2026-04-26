using KwikNesta.Mediator.Cores.Abstractions;
using KwikNesta.Shared.Responses;
using KwikNesta.Shared.ServiceDTOs.Payment;

namespace KwikNesta.Shared.ServiceQueries.Payment
{
    public record GetPaystackBanksQuery() : IKNRequest<Response<PaystackBanksResponse>>;
}