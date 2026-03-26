using KwikNesta.Mediator.Cores.Abstractions;
using KwikNesta.Shared.Responses;
using KwikNesta.Shared.ServiceDTOs.Infra;

namespace KwikNesta.Shared.ServiceQueries.Infra
{
    public class GetCitiesByCountryAndStateQuery 
        : GetCitiesByCountryAndStateClientQuery, IKNRequest<PagedResponse<CityDto>>
    {
        public Guid StateId { get; set; }
        public Guid CountryId { get; set; }
    }

    public class GetCitiesByCountryAndStateClientQuery : BasePageQuery
    {
        public string? Search { get; set; }
    }
}