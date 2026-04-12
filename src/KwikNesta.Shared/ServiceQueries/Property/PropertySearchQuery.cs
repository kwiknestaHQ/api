using KwikNesta.Mediator.Cores.Abstractions;
using KwikNesta.Shared.Models.Enumerations.Property;
using KwikNesta.Shared.Responses;
using KwikNesta.Shared.ServiceDTOs.Property;

namespace KwikNesta.Shared.ServiceQueries.Property
{
    public class PropertySearchQuery : BasePageQuery, IKNRequest<PagedResponse<PropertyCardDto>>
    {
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public double RadiusKm { get; set; } = 5;

        public string? City { get; set; }
        //public string? State { get; set; }
        //public string? SearchText { get; set; }

        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public int? Bedrooms { get; set; }
        public int? Bathrooms { get; set; }
        public EPropertyType? Type { get; set; }
        public EListingType? ListingType { get; set; }
    }
}