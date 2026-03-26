using KwikNesta.Mediator.Cores.Abstractions;
using KwikNesta.Shared.Responses;
using KwikNesta.Shared.ServiceDTOs.Infra;
using KwikNesta.Shared.ServiceQueries.Infra;
using KwikNestaInfra.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace KwikNestaInfra.Application.Handlers
{
    public class GetStateByIdQueryHandler(IInfraRepositoryManager repository)
        : IKNRequestHandler<GetStateByIdQuery, Response<StateDto>>
    {
        private readonly IInfraRepositoryManager _repository = repository;

        public async Task<Response<StateDto>> HandleAsync(GetStateByIdQuery request, CancellationToken cancellationToken)
        {
            var state = await _repository.State.Get(s => s.CountryId == request.CountryId && 
                                s.Id == request.Id &&
                                !s.IsDeprecated)
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
                }).FirstOrDefaultAsync(cancellationToken);

            if(state == null)
            {
                return Response<StateDto>.Fail(InfraResponses.RecordNotFound, 404);
            }

            return Response<StateDto>.Ok(state);
        }
    }
}