using KwikNesta.Mediator.Cores.Abstractions;
using KwikNesta.Shared.Helpers;
using KwikNesta.Shared.Models.Enumerations.Property;
using KwikNesta.Shared.Models.Settings;
using KwikNesta.Shared.Responses;
using KwikNesta.Shared.ServiceCommands.Property;
using KwikNesta.Shared.ServiceDTOs.Property;
using KwikNestaProperty.Domain.Entities;
using KwikNestaProperty.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace KwikNestaProperty.Application.Handlers
{
    public class ViewingCheckInCommandHandler(IPropertyRepositoryManager repository,
        IOptions<KNApplicationSettings> options) : IKNRequestHandler<ViewingCheckInCommand, Response<ViewingCheckInResponse>>
    {
        private readonly IPropertyRepositoryManager _repository = repository;
        private readonly ViewingCheckInSettings _settings = options.Value.CheckIn ??
                throw new ArgumentNullException(nameof(ViewingCheckInSettings));

        public async Task<Response<ViewingCheckInResponse>> HandleAsync(ViewingCheckInCommand request, CancellationToken cancellationToken)
        {
           if (request == null || request.UserContext == null || 
                string.IsNullOrWhiteSpace(request.UserContext.Id))
           {
                return Response<ViewingCheckInResponse>.Fail(PropertyResponse.InvalidRequest, 
                    StatusCodes.Status400BadRequest);
           }

            var alreadyCheckedIn = await _repository.ViewingCheckIn
                .ExistsAsync(c => c.ViewingRequestId == request.ViewingRequestId
                            && c.UserId == request.UserContext.Id);

            if (alreadyCheckedIn)
            {
                return Response<ViewingCheckInResponse>.Fail(PropertyResponse.AlreadyCheckedIn,
                    StatusCodes.Status409Conflict);
            }

            var viewingRequest = await _repository.ViewingRequest
                 .FirstOrDefault(r => r.Id == request.ViewingRequestId);

            if (viewingRequest == null)
            {
                return Response<ViewingCheckInResponse>.Fail(
                    string.Format(PropertyResponse.RecordNotFound, "Viewing Request"),
                    StatusCodes.Status404NotFound);
            }

            var viewingSession = await _repository.ViewingSession
                 .FirstOrDefault(r => r.Id == viewingRequest.Id, true);

            if (viewingSession == null)
            {
                return Response<ViewingCheckInResponse>.Fail(
                    string.Format(PropertyResponse.RecordNotFound, "Viewing Session"),
                    StatusCodes.Status404NotFound);
            }

            var participant = await _repository.SessionParticipant
                 .FirstOrDefault(r => r.Id == viewingSession.Id && 
                    r.UserId == request.UserContext.Id, true);

            if (participant == null)
            {
                return Response<ViewingCheckInResponse>.Fail(
                    string.Format(PropertyResponse.RecordNotFound, "Participant"),
                    StatusCodes.Status404NotFound);
            }

            var now = DateTimeOffset.UtcNow;
            var windowStart = viewingSession.ScheduledStart.AddMinutes(-_settings.WindowMinutesBefore);
            var windowEnd = viewingSession.ScheduledStart.AddMinutes(_settings.WindowMinutesAfter);

            if (now < windowStart || now > windowEnd)
            {
                return Response<ViewingCheckInResponse>.Fail(
                    string.Format(PropertyResponse.CheckInNotPermitted, $"{windowStart:HH:mm}", $"{ windowEnd:HH:mm}"),
                    StatusCodes.Status400BadRequest);
            }

            if (request.AccuracyMeters > _settings.MaxAccuracyMeters)
            {
                return Response<ViewingCheckInResponse>.Fail(
                    string.Format(PropertyResponse.GPSAccuracyLow, $"{request.AccuracyMeters:F0}"),
                    StatusCodes.Status400BadRequest);
            }

            var propertyLocation = await _repository.PropertyLocation
                .FirstOrDefault(r => r.PropertyId == viewingRequest.PropertyId);

            if (propertyLocation == null)
            {
                return Response<ViewingCheckInResponse>.Fail(
                    string.Format(PropertyResponse.RecordNotFound, "Property Location"),
                    StatusCodes.Status404NotFound);
            }

            var distance = LocationHelpers.DistanceMeters(
                propertyLocation.Latitude, propertyLocation.Longitude,
                request.Latitude, request.Longitude);

            if (distance > _settings.ThresholdMeters)
            {
                return Response<ViewingCheckInResponse>.Fail(
                   string.Format(PropertyResponse.TooFarFromPropertyToCheckIn, $"{distance:F0}", $"{_settings.ThresholdMeters}"),
                   StatusCodes.Status400BadRequest);
            }

            participant.MarkAsJoined(DateTime.UtcNow);
            if(viewingSession.Status != EViewingSessionStatus.InProgress)
            {
                viewingSession.MarkAsInProgress(DateTime.UtcNow);
            }

            var checkIn = new ViewingCheckIn
            {
                ViewingRequestId = request.ViewingRequestId,
                UserId = request.UserContext.Id,
                UserLatitude = request.Latitude,
                UserLongitude = request.Longitude,
                GpsAccuracyMeters = request.AccuracyMeters,
                DistanceMeters = distance,
                IpAddress = request.UserContext.IpAddress
            };

            await _repository.ViewingCheckIn.AddAsync(checkIn);
            await _repository.SaveAsync();
            return Response<ViewingCheckInResponse>.Ok(new ViewingCheckInResponse
            {
                Id = checkIn.Id,
                CheckedInAt = checkIn.CreatedOn,
                DistanceMeters = checkIn.DistanceMeters
            });
        }
    }
}