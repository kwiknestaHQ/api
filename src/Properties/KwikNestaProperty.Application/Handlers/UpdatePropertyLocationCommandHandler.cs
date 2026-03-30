using Hangfire;
using KwikNesta.Mediator.Cores.Abstractions;
using KwikNesta.Shared.Helpers;
using KwikNesta.Shared.Implementations;
using KwikNesta.Shared.Models.Enumerations.Infra;
using KwikNesta.Shared.Models.Enumerations.Property;
using KwikNesta.Shared.Responses;
using KwikNesta.Shared.ServiceCommands.Property;
using KwikNesta.Shared.ServiceQueries.Infra;
using KwikNestaProperty.Application.Validations;
using KwikNestaProperty.Infrastructure;
using KwikNestaProperty.Infrastructure.Services;

namespace KwikNestaProperty.Application.Handlers
{
    public class UpdatePropertyLocationCommandHandler(IPropertyRepositotyManager repository, 
                                                    IKNMediator mediator) 
        : IKNRequestHandler<UpdatePropertyLocationCommand, Response<string>>
    {
        private readonly IPropertyRepositotyManager _repository = repository;
        private readonly IKNMediator _mediator = mediator;
        private const double MaxDistanceTolerance = 0.2;

        public async Task<Response<string>> HandleAsync(UpdatePropertyLocationCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdatePropertyLocationCommandValidator().Validate(request);
            if (!validator.IsValid)
            {
                return Response<string>
                    .Fail(validator.Errors.FirstOrDefault()?.ErrorMessage ?? PropertyResponse.InvalidRequest, 400);
            }

            var location = await _repository.PropertyLocation
                    .FirstOrDefault(pl => pl.Id == request.PropertyId, true);
            if (location == null)
            {
                return Response<string>
                        .Fail(string.Format(PropertyResponse.RecordNotFound, "Property Location"), 404);
            }

            var locationChanged = LocationHelpers.GetDistance(request.Latitude,
                    request.Longitude,
                    location.Latitude,
                    location.Longitude) > MaxDistanceTolerance;

            var stateResponse = await _mediator.SendAsync(new GetStateByIdQuery
            {
                CountryId = request.CountryId,
                Id = request.StateId
            }, cancellationToken);

            if (!stateResponse.Success)
            {
                return Response<string>.Fail(stateResponse.Message, stateResponse.StatusCode);
            }

            if (stateResponse.Data == null || string.IsNullOrWhiteSpace(stateResponse.Data.Name) ||
                string.IsNullOrWhiteSpace(stateResponse.Data.CountryName))
            {
                return Response<string>.Fail(PropertyResponse.LocationInfoRequired, 400);
            }

            location.State = stateResponse.Data.Name;
            location.Country = stateResponse.Data.CountryName;
            location.Latitude = request.Latitude;
            location.Longitude = request.Longitude;
            location.City = request.City;
            location.Address = request.Address;
            location.LastUpdatedOn = DateTime.UtcNow;

            if (locationChanged)
            {
                location.VerificationStatus = ELocationVerificationStatus.RequiresReverification;
            }

            await _repository.SaveAsync();
            if(location.VerificationStatus == ELocationVerificationStatus.RequiresReverification)
            {
                BackgroundJob.Enqueue<BackgroundLocationVerificationService>(s
                   => s.Verify(location.PropertyId, null!));
            }

            AppAudit.Write(request.UserContext.Id,
                request.UserContext.Email,
                EAuditAction.UpdatedPropertyLocation,
                EAuditDomain.Property,
                location.PropertyId.ToString(),
                request.UserContext.IpAddress,
                locationChanged ? "Location Coordinates Changed" : null);

            return Response<string>.Ok(PropertyResponse.PropertyLocationUpdated);
        }
    }
}