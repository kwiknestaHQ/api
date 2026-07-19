using KwikNesta.Mediator.Cores.Abstractions;
using KwikNesta.Shared.Models.Enumerations.Property;
using KwikNesta.Shared.Models.Settings;
using KwikNesta.Shared.Responses;
using KwikNesta.Shared.ServiceDTOs.Property;
using KwikNesta.Shared.ServiceQueries.Property;
using KwikNestaProperty.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace KwikNestaProperty.Application.Handlers
{
    public class GetViewingCheckInPreviewQueryHandler(IPropertyRepositoryManager repository, IOptions<KNApplicationSettings> options)
        : IKNRequestHandler<ViewingCheckInPreviewQuery, Response<ViewingRequestCheckInPreview>>
    {
        private readonly IPropertyRepositoryManager _repository = repository;
        private readonly ViewingCheckInSettings _settings = options.Value.CheckIn ??
                throw new ArgumentNullException(nameof(ViewingCheckInSettings));

        public async Task<Response<ViewingRequestCheckInPreview>> HandleAsync(ViewingCheckInPreviewQuery request, CancellationToken cancellationToken)
        {
            var viewing = await _repository.ViewingRequest
                .Get(v => v.Id == request.ViewingRequestId)
                .Include(v => v.Property)
                .Include(v => v.Session)
                .Select(v => new
                {
                    v.Id,
                    ScheduledAt = v.Session.ScheduledStart,
                    v.PropertyId,
                    Property = new
                    {
                        v.Property.Location.City,
                        v.Property.Location.Address,
                        Image = v.Property.Media.Where(i => i.IsPrimary && i.Type == EMediaType.Image)
                        .Select(i => i.Url)
                        .FirstOrDefault()
                    }
                })
                .FirstOrDefaultAsync(cancellationToken);

            if (viewing == null)
            {
                return Response<ViewingRequestCheckInPreview>.Fail(
                    string.Format(PropertyResponse.RecordNotFound, "Viewing Request"),
                    StatusCodes.Status404NotFound);
            }
       
            var now = DateTimeOffset.UtcNow;
            var windowStart = viewing.ScheduledAt.AddMinutes(_settings.WindowMinutesBefore);
            var windowEnd = viewing.ScheduledAt.AddMinutes(_settings.WindowMinutesAfter);
            var checkedIn = await _repository.ViewingCheckIn
                .ExistsAsync(c => c.ViewingRequestId == request.ViewingRequestId && 
                    c.UserId == request.UserId);

            return Response<ViewingRequestCheckInPreview>.Ok(new ViewingRequestCheckInPreview
            {
                Id = viewing.Id,
                ScheduledAt = viewing.ScheduledAt,
                AlreadyCheckeIn = checkedIn,
                Property = new ViewingRequestCheckInPreviewProperty
                {
                    Address = viewing.Property.Address,
                    City = viewing.Property.City,
                    PhotoUrl = viewing.Property.Image!
                },
                WindowStart = windowStart,
                WindowEnd = windowEnd,
                ThresholdMeters = _settings.ThresholdMeters,
                MaxAccuracyMeters = _settings.MaxAccuracyMeters
            });
        }
    }
}