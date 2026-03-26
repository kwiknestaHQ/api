using KwikNesta.Mediator.Cores.Abstractions;
using KwikNesta.Shared.Responses;
using KwikNesta.Shared.ServiceDTOs.Infra;
using KwikNesta.Shared.ServiceQueries.Infra;
using KwikNestaInfra.Infrastructure;

namespace KwikNestaInfra.Application.Handlers
{
    public class GetStatesByCountryQueryHandler(IInfraRepositoryManager repository) 
        : IKNRequestHandler<GetStatesByCountryQuery, PagedResponse<StateDto>>
    {
        private readonly IInfraRepositoryManager _repository = repository;

        public async Task<PagedResponse<StateDto>> HandleAsync(GetStatesByCountryQuery request, CancellationToken cancellationToken)
        {
            var statesQuery = _repository.State.Get(s => s.CountryId == request.CountryId && !s.IsDeprecated);
            if (!string.IsNullOrWhiteSpace(request.Search))
            {
                var search = request.Search.ToLower();
                statesQuery = statesQuery.Where(s => 
                                    s.Name.ToLower().Contains(search) || 
                                    s.ISO2.ToLower().Contains(search));
            }
            
            var data = statesQuery.OrderBy(s => s.Name)
                .Select(s => new StateDto
                {
                    Id = s.Id,
                    Name = s.Name,
                    CountryCode = s.CountryCode,
                    CountryId = s.CountryId,
                    CountryName = s.Country != null ? s.Country.Name : null,
                    ISO2 = s.ISO2,
                    Type = s.Type,
                    Longitude = s.Longitude,
                    Latitude = s.Latitude,
                }).Paginate(request.Page, request.PageSize);

            await Task.CompletedTask;
            return data;
        }
    }
}