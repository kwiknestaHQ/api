using Hangfire;
using KwikNesta.Mediator.Cores.Abstractions;
using KwikNesta.Shared.Extensions;
using KwikNesta.Shared.Models;
using KwikNesta.Shared.Models.Enumerations.Property;
using KwikNesta.Shared.Responses;
using KwikNesta.Shared.ServiceDTOs.Property;
using KwikNesta.Shared.ServiceQueries.Identity;
using KwikNesta.Shared.ServiceQueries.Property;
using KwikNestaProperty.Domain.Entities;
using KwikNestaProperty.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace KwikNestaProperty.Application.Handlers
{
    public class GetPropertyDetailsQueryHandler(IPropertyRepositotyManager repository, 
                                            IKNMediator mediator) 
        : IKNRequestHandler<GetPropertyDetailsQuery, Response<PropertyDetailsDto>>
    {
        private readonly IPropertyRepositotyManager _repository = repository;
        private readonly IKNMediator _mediator = mediator;

        public async Task<Response<PropertyDetailsDto>> HandleAsync(GetPropertyDetailsQuery request, CancellationToken cancellationToken)
        {
            var property = await _repository.Property
                .Get(p => p.Id == request.Id)
                .Select(p => new PropertyDetailsDto
                {
                    Id = p.Id,
                    Title = p.Title,
                    Description = p.Description,
                    AreaSize = p.AreaSize,
                    AreaUnit = p.AreaUnit,
                    Bathrooms = p.Bathrooms,
                    Bedrooms = p.Bedrooms,
                    Currency = p.Currency,
                    Price = p.Price,
                    ParkingSpaces = p.ParkingSpaces,
                    IsVerified = p.Status == EListingStatus.Available && 
                        !p.IsDeprecated &&
                        p.Location.VerificationStatus == ELocationVerificationStatus.Verified &&
                        p.OwnershipVerificationRequests
                            .OrderByDescending(o => o.CreatedOn)
                            .Select(o => o.Status)
                            .FirstOrDefault() == EVerificationStatus.Approved,
                    Type = p.Type,
                    ListingType = p.ListingType,
                    PriceFrequency = p.PriceFrequency.GetDescription(),
                    Media = p.Media.Select(m => new PropertyMediaDto
                    {
                        Url = m.Url,
                        IsPrimary = m.IsPrimary,
                        Type = m.Type
                    }).ToList(),
                    Location = new PropertyLocationDto
                    {
                        Address = p.Location.Address,
                        City = p.Location.City,
                        Latitude = p.Location.Latitude,
                        Longitude = p.Location.Longitude,
                        IsVerified = p.Location.VerificationStatus == ELocationVerificationStatus.Verified
                    },
                    CreatedAt = p.CreatedOn,
                    OwnerId = p.OwnerId
                }).FirstOrDefaultAsync(cancellationToken);

            if (property == null)
            {
                return Response<PropertyDetailsDto>.Fail(string.Format(PropertyResponse.RecordNotFound, "Property"), 
                    StatusCodes.Status404NotFound);
            }

            var ownerInfo = await _mediator.SendAsync(new GetUserByIdQuery
            {
                Id = property.OwnerId
            }, cancellationToken);
            if (!ownerInfo.Success)
            {
                return Response<PropertyDetailsDto>.Fail(ownerInfo.Message, ownerInfo.StatusCode);
            }

            property.Landlord = new LandlordDto
            {
                Id = ownerInfo.Data.Id,
                FirstName = ownerInfo.Data.FirstName,
                IsVerified = ownerInfo.Data.IsVerified
            };

            
            property.Features = await _repository.PropertyFeatureLink
                .Get(x => x.PropertyId == request.Id)
                .Select(x => x.Feature != null
                    ? x.Feature.Name
                    : x.CustomFeature!)
                .ToListAsync();

            property.ViewsCount = await _repository.PropertyView
                .CountAsync(v => v.PropertyId == request.Id);

            if(property.OwnerId != request.Context.Id)
            {
                BackgroundJob.Enqueue(()
                    => TryAddUseriew(request.Id, request.Context));
            }

            return Response<PropertyDetailsDto>.Ok(property);
        }

        public async Task TryAddUseriew(Guid propertyId, UserContext context)
        {
            var now = DateTime.UtcNow;
            var window = now.AddMinutes(-30);

            var existing = await _repository.PropertyView
                .ExistsAsync(v =>
                    v.PropertyId == propertyId &&
                    (
                        (context.Id != null && v.UserId == context.Id) ||
                        (context.Id == null && v.IpAddress == context.IpAddress)
                    ) &&
                    v.ViewedAt >= window
                );

            if (!existing)
            {
                await _repository.PropertyView.AddAsync(new PropertyView
                {
                    PropertyId = propertyId,
                    UserId = context.Id,
                    IpAddress = context.IpAddress,
                    ViewedAt = now
                });

                await _repository.SaveAsync();
            }
        }
    }
}