using Hangfire;
using KwikNesta.Mediator.Cores.Abstractions;
using KwikNesta.Shared.Contracts;
using KwikNesta.Shared.Implementations;
using KwikNesta.Shared.Models.Enumerations.Infra;
using KwikNesta.Shared.Responses;
using KwikNesta.Shared.ServiceCommands.Property;
using KwikNesta.Shared.ServiceDTOs.Property;
using KwikNesta.Shared.ServiceQueries.Infra;
using KwikNestaProperty.Application.Validations;
using KwikNestaProperty.Infrastructure;
using KwikNestaProperty.Infrastructure.Services;

namespace KwikNestaProperty.Application.Handlers
{
    public class CreatePropertyCommandHandler(IPropertyRepositotyManager repository,
                                            IKNMediator mediator, 
                                            IReverseGeocodeService geocodeService) 
        : IKNRequestHandler<CreatePropertyCommand, Response<CreatePropertyResponseDto>>
    {
        private readonly IPropertyRepositotyManager _repository = repository;
        private readonly IKNMediator _mediator = mediator;
        private readonly IReverseGeocodeService _geocodeService = geocodeService;

        public async Task<Response<CreatePropertyResponseDto>> HandleAsync(CreatePropertyCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreatePropertyCommandValidator().Validate(request);
            if (!validator.IsValid)
            {
                return Response<CreatePropertyResponseDto>.Fail(validator.Errors.FirstOrDefault()?.ErrorMessage ?? 
                    PropertyResponse.InvalidRequest, 400);
            }

            var countryResponse = await _mediator.SendAsync(new GetCountryByIdQuery { Id = request.Location.CountryId }, cancellationToken);
            if (!countryResponse.Success)
            {
                return Response<CreatePropertyResponseDto>.Fail(countryResponse.Message, countryResponse.StatusCode);
            }

            var stateResponse = await _mediator.SendAsync(new GetStateByIdQuery
            {
                CountryId = request.Location.CountryId,
                Id = request.Location.StateId
            }, cancellationToken);

            if (!stateResponse.Success)
            {
                return Response<CreatePropertyResponseDto>.Fail(stateResponse.Message, stateResponse.StatusCode);
            }

            if(stateResponse.Data == null || countryResponse.Data == null || string.IsNullOrWhiteSpace(stateResponse.Data.Name) || 
                string.IsNullOrWhiteSpace(countryResponse.Data.Name) || string.IsNullOrWhiteSpace(countryResponse.Data.Currency))
            {
                return Response<CreatePropertyResponseDto>.Fail(PropertyResponse.LocationInfoRequired, 400);
            }

            var propertyToAdd = request.Map(countryResponse.Data.Currency);
            await _repository.BeginTransaction(async () =>
            {
                await _repository.Property.AddAsync(propertyToAdd);
                await _repository.PropertyLocation.AddAsync(request.Location.MapLocation(propertyToAdd.Id,
                            stateResponse.Data.Name, 
                            countryResponse.Data.Name));
            });

            AppAudit.Write(request.UserContext.Id,
                request.UserContext.Email,
                EAuditAction.AddedProperty,
                EAuditDomain.Property,
                propertyToAdd.Id.ToString(),
                request.UserContext.IpAddress);

            BackgroundJob.Enqueue<BackgroundLocationVerificationService>(s
                => s.Verify(propertyToAdd.Id, null!));

            return Response<CreatePropertyResponseDto>.Ok(new CreatePropertyResponseDto(propertyToAdd.Id, propertyToAdd.Status));
        }
    }
}