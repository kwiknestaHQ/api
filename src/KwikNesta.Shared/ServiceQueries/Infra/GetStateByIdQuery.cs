using KwikNesta.Mediator.Cores.Abstractions;
using KwikNesta.Shared.Responses;
using KwikNesta.Shared.ServiceDTOs.Infra;

namespace KwikNesta.Shared.ServiceQueries.Infra
{
    public class GetStateByIdQuery : IKNRequest<Response<StateDto>>
    {
        public Guid Id { get; set; }
        public Guid CountryId { get; set; }
    }
}