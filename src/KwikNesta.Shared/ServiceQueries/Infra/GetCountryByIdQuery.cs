using KwikNesta.Mediator.Cores.Abstractions;
using KwikNesta.Shared.Responses;
using KwikNesta.Shared.ServiceDTOs.Infra;

namespace KwikNesta.Shared.ServiceQueries.Infra
{
    public class GetCountryByIdQuery : IKNRequest<Response<CountryDto>>
    {
        public Guid Id { get; set; }
    }
}