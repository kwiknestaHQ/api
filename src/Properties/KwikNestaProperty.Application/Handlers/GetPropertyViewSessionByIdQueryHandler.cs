using KwikNesta.Mediator.Cores.Abstractions;
using KwikNesta.Shared.Extensions;
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
    public class GetPropertyViewSessionByIdQueryHandler(IPropertyRepositoryManager repository, 
                        IOptions<KNApplicationSettings> options)
        : IKNRequestHandler<GetPropertyViewSessionByIdQuery, Response<ViewRequestSessionDetailsDto>>
    {
        private readonly IPropertyRepositoryManager _repository = repository;
        private readonly AgoraSetting _agoraSetting = options.Value.Agora ?? 
            throw new ArgumentNullException(nameof(AgoraSetting));

        public async Task<Response<ViewRequestSessionDetailsDto>> HandleAsync(GetPropertyViewSessionByIdQuery request, CancellationToken cancellationToken)
        {
            if(request.SessionId == Guid.Empty)
            {
                return Response<ViewRequestSessionDetailsDto>.Fail(PropertyResponse.InvalidRequest,
                    StatusCodes.Status400BadRequest);
            }

            var session = await _repository.ViewingSession
                .Get(s => s.Id == request.SessionId)
                .Include(s => s.ViewingRequest)
                .FirstOrDefaultAsync(cancellationToken);

            if(session == null || session.ViewingRequest == null)
            {
                return Response<ViewRequestSessionDetailsDto>.Fail(
                    string.Format(PropertyResponse.RecordNotFound, "View Session"),
                    StatusCodes.Status404NotFound);
            }

            var property = await _repository.Property
                .Get(p => p.Id == session.ViewingRequest.PropertyId)
                .Include(p => p.Location)
                .Include(p => p.Media)
                .Select(p => new
                {
                    Address = $"{p.Location.Address}, {p.Location.City}, {p.Location.State}.",
                    Type = p.Type.GetDescription(),
                    Image = p.Media.Where(i => i.IsPrimary && i.Type == EMediaType.Image)
                        .Select(i => i.Url)
                        .FirstOrDefault()
                }).FirstOrDefaultAsync(cancellationToken);

            if(property == null)
            {
                return Response<ViewRequestSessionDetailsDto>.Fail(
                    string.Format(PropertyResponse.RecordNotFound, "Property"),
                    StatusCodes.Status404NotFound);
            }

            return Response<ViewRequestSessionDetailsDto>.Ok(new ViewRequestSessionDetailsDto
            {
                Id = session.Id,
                PropertyAddress = property.Address,
                PropertyPhoto = property.Image!,
                PropertyType = property.Type,
                ScheduledAt = session.ScheduledStart,
                InspectionType = session.ViewingRequest.Type.GetDescription(),
                Status = session.Status.GetDescription(),
                EstimatedDurationMinutes = (_agoraSetting.MinAttendanceDurationSeconds / 60),
            });
        }
    }
}