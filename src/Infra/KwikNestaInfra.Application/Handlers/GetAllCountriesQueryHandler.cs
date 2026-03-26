using KwikNesta.Mediator.Cores.Abstractions;
using KwikNesta.Shared.Responses;
using KwikNesta.Shared.ServiceDTOs.Infra;
using KwikNesta.Shared.ServiceQueries.Infra;
using KwikNestaInfra.Infrastructure;

namespace KwikNestaInfra.Application.Handlers
{
    public class GetAllCountriesQueryHandler(IInfraRepositoryManager repository) 
        : IKNRequestHandler<GetAllCountriesQuery, PagedResponse<CountryDto>>
    {
        private readonly IInfraRepositoryManager _repository = repository;

        public async Task<PagedResponse<CountryDto>> HandleAsync(GetAllCountriesQuery request, CancellationToken cancellationToken)
        {
            var countryQuery = _repository.Country.Get(c => true);
            if (request.OnlyActive)
            {
                countryQuery = countryQuery.Where(x => x.IsActive);
            }

            if (!string.IsNullOrWhiteSpace(request.Search))
            {
                var search = request.Search.ToLower();
                countryQuery = countryQuery
                    .Where(c => c.Name.ToLower().Contains(search) || c.ISO2.ToLower().Contains(search));
            }

            var data = countryQuery.OrderBy(c => c.Name)
                .Select(c => ObjectFactory.Map(c))
                .Paginate(request.Page, request.PageSize);

            await Task.CompletedTask;
            return data;
        }
    }
}
