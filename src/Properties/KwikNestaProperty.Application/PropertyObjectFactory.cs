using KwikNesta.Shared.Models.Enumerations.Property;
using KwikNesta.Shared.ServiceCommands.Property;
using KwikNestaProperty.Domain.Entities;

namespace KwikNestaProperty.Application
{
    internal static class PropertyObjectFactory
    {
        public static KNProperty Map(this CreatePropertyCommand request, 
                                    string currency)
        {
            return new KNProperty
            {
                Title = request.Title,
                Description = request.Description,
                Price = request.Price,
                Currency = currency,
                Type = request.Type,
                ListingType = request.ListingType,
                PriceFrequency = request.PriceFrequency,

                Bedrooms = request.Bedrooms,
                Bathrooms = request.Bathrooms,
                AreaSize = request.AreaSize,
                ParkingSpaces = request.ParkingSpaces,

                OwnerId = request.UserContext.Id,
                Status = EListingStatus.Draft
            };
        }

        public static PropertyLocation MapLocation(this CreateLocationDto request, 
                                                Guid propertyId,
                                                string state,
                                                string country)
        {
            return new PropertyLocation
            {
                PropertyId = propertyId,
                Address = request.Address,
                City = request.City,
                State = state,
                Country = country,
                Latitude = request.Latitude,
                Longitude = request.Longitude
            };
        }
    }
}