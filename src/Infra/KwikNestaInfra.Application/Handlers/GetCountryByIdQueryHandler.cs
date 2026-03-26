using KwikNesta.Mediator.Cores.Abstractions;
using KwikNesta.Shared.Responses;
using KwikNesta.Shared.ServiceDTOs.Infra;
using KwikNesta.Shared.ServiceQueries.Infra;
using KwikNestaInfra.Infrastructure;

namespace KwikNestaInfra.Application.Handlers
{
    public class GetCountryByIdQueryHandler(IInfraRepositoryManager repository) : IKNRequestHandler<GetCountryByIdQuery, Response<CountryDto>>
    {
        private readonly IInfraRepositoryManager _repository = repository;

        public async Task<Response<CountryDto>> HandleAsync(GetCountryByIdQuery request, CancellationToken cancellationToken)
        {
            var country = await _repository.Country.FirstOrDefault(c => c.Id == request.Id && !c.IsDeprecated);
            if(country == null)
            {
                return Response<CountryDto>.Fail(InfraResponses.RecordNotFound, 404);
            }

            return Response<CountryDto>.Ok(ObjectFactory.Map(country));
        }
    }
}