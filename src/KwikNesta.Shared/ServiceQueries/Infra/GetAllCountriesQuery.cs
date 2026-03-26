using KwikNesta.Mediator.Cores.Abstractions;
using KwikNesta.Shared.Responses;
using KwikNesta.Shared.ServiceDTOs.Infra;

namespace KwikNesta.Shared.ServiceQueries.Infra
{
    public class GetAllCountriesQuery 
        : BasePageQuery, IKNRequest<PagedResponse<CountryDto>>
    {
        public string? Search { get; set; }
        public bool OnlyActive { get; set; } = true;
    }
}
