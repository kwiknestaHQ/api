using KwikNesta.Mediator.Cores.Abstractions;
using KwikNesta.Shared.Responses;
using KwikNesta.Shared.ServiceDTOs.Infra;
using KwikNesta.Shared.ServiceQueries.Infra;
using KwikNestaInfra.Infrastructure;

namespace KwikNestaInfra.Application.Handlers
{
    public class GetCitiesByCountryAndStateQueryHandler(IInfraRepositoryManager repository)
        : IKNRequestHandler<GetCitiesByCountryAndStateQuery, PagedResponse<CityDto>>
    {
        private readonly IInfraRepositoryManager _repository = repository;

        public async Task<PagedResponse<CityDto>> HandleAsync(GetCitiesByCountryAndStateQuery request, CancellationToken cancellationToken)
        {
            var citiesQuery = _repository.City.Get(s => s.StateId == request.StateId && 
                                                        s.CountryId == request.CountryId && 
                                                        !s.IsDeprecated);
            if (!string.IsNullOrWhiteSpace(request.Search))
            {
                var search = request.Search.ToLower();
                citiesQuery = citiesQuery.Where(s =>
                                    s.Name.ToLower().Contains(search));
            }

            var data = citiesQuery.OrderBy(s => s.Name)
                .Select(s => new CityDto
                {
                    Id = s.Id,
                    Name = s.Name,
                    CountryId = s.CountryId,
                    CountryName = s.Country != null ? s.Country.Name : null,
                    StateId = s.StateId,
                    StateName = s.State != null ? s.State.Name : null,
                    Longitude = s.Longitude,
                    Latitude = s.Latitude,
                }).Paginate(request.Page, request.PageSize);

            await Task.CompletedTask;
            return data;
        }
    }
}