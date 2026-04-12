using KwikNesta.Mediator.Cores.Abstractions;
using KwikNesta.Shared.Helpers;
using KwikNesta.Shared.Models.Enumerations.Property;
using KwikNesta.Shared.Responses;
using KwikNesta.Shared.ServiceDTOs.Property;
using KwikNesta.Shared.ServiceQueries.Property;
using KwikNestaProperty.Infrastructure;

namespace KwikNestaProperty.Application.Handlers
{
    public class PropertySearchQueryHandler(IPropertyRepositotyManager repository) 
        : IKNRequestHandler<PropertySearchQuery, PagedResponse<PropertyCardDto>>
    {
        private readonly IPropertyRepositotyManager _repository = repository;

        public async Task<PagedResponse<PropertyCardDto>> HandleAsync(PropertySearchQuery request, CancellationToken cancellationToken)
        {
            var query = _repository.Property
                .Get(PropertyExpressions.IsVerified());

            if (request.MinPrice.HasValue)
            {
                query = query.Where(p => p.Price >= request.MinPrice);
            }

            if (request.MaxPrice.HasValue)
            {
                query = query.Where(p => p.Price <= request.MaxPrice);
            }

            if (request.Bedrooms.HasValue)
            {
                query = query.Where(p => p.Bedrooms == request.Bedrooms);
            }

            if (request.Bathrooms.HasValue)
            {
                query = query.Where(p => p.Bathrooms == request.Bathrooms);
            }

            if (request.ListingType.HasValue)
            {
                query = query.Where(p => p.ListingType == request.ListingType);
            }

            if (request.Type.HasValue)
            {
                query = query.Where(p => p.Type == request.Type);
            }

            var isLocationSearch = request.Latitude.HasValue && request.Longitude.HasValue;
            if (isLocationSearch)
            {
                var lat = request.Latitude!.Value;
                var lng = request.Longitude!.Value;
                var radius = request.RadiusKm;
                var (minLat, maxLat, minLng, maxLng) =
                    LocationHelpers.GetBoundingBox(lat, lng, radius);

                query = query.Where(p =>
                    p.Location.Latitude >= minLat &&
                    p.Location.Latitude <= maxLat &&
                    p.Location.Longitude >= minLng &&
                    p.Location.Longitude <= maxLng);
            }
            else if (!string.IsNullOrWhiteSpace(request.City))
            {
                var normalizedCity = request.City.ToLower();
                query = query.Where(p => p.Location.City.ToLower().Contains(normalizedCity));
            }

            var projectedQuery = query
                .Select(p => new
                {
                    p.Id,
                    p.Title,
                    p.Price,
                    p.Bedrooms,
                    City = p.Location.City,
                    Lat = p.Location.Latitude,
                    Lng = p.Location.Longitude,
                    CoverImageUrl = p.Media
                        .Where(i => i.IsPrimary && i.Type == EMediaType.Image)
                        .Select(i => i.Url)
                        .FirstOrDefault()
                });

            var paged = await projectedQuery
                .PaginateAsync(request.Page, request.PageSize, cancellationToken);

            var result = paged.Items.Select(p => new PropertyCardDto
            {
                Id = p.Id,
                Title = p.Title,
                Price = p.Price,
                Bedrooms = p.Bedrooms,
                City = p.City,
                CoverImageUrl = p.CoverImageUrl,
                DistanceKm = isLocationSearch
                    ? LocationHelpers.GetDistance(request.Latitude!.Value, request.Longitude!.Value, p.Lat, p.Lng)
                    : null
            }).ToList();

            return new PagedResponse<PropertyCardDto>(
                result,
                paged.Page,
                request.PageSize,
                paged.Total
            );
        }
    }
}