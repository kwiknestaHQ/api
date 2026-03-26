using KwikNesta.Mediator.Cores.Abstractions;
using KwikNesta.Shared.Responses;
using KwikNesta.Shared.ServiceDTOs.Infra;

namespace KwikNesta.Shared.ServiceQueries.Infra
{
    public class GetStatesByCountryQuery : GetStatesByCountryClientQuery, IKNRequest<PagedResponse<StateDto>>
    {
        public Guid CountryId { get; set; }
    }

    public class GetStatesByCountryClientQuery : BasePageQuery
    {
        public string? Search { get; set; }
    }
}