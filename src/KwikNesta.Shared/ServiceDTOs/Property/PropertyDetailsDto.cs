using KwikNesta.Shared.Extensions;
using KwikNesta.Shared.Models.Enumerations.Property;
using System.Text.Json.Serialization;

namespace KwikNesta.Shared.ServiceDTOs.Property
{
    public class PropertyDetailsDto
    {
        public Guid Id { get; set; }
        [JsonIgnore]
        public string OwnerId { get; set; } = default!;
        public string Title { get; set; } = default!;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public string Currency { get; set; } = default!;
        public string PriceFrequency { get; set; } = default!;

        public int Bedrooms { get; set; }
        public int Bathrooms { get; set; }
        public double AreaSize { get; set; }
        public string AreaUnit { get; set; } = default!;
        public int? ParkingSpaces { get; set; }

        public EPropertyType Type { get; set; }
        public string TypeText => Type.GetDescription();
        public EListingType ListingType { get; set; }
        public string ListingTypeText => ListingType.GetDescription();
        public List<string> Features { get; set; } = [];
        public List<PropertyMediaDto> Media { get; set; } = [];
        public PropertyLocationDto Location { get; set; } = default!;
        public LandlordDto Landlord { get; set; } = default!;
        public bool IsVerified { get; set; }
        public DateTime CreatedAt { get; set; }
        public int ViewsCount { get; set; }
        //public bool CanRequestViewing { get; set; }
        //public bool CanMessageLandlord { get; set; }
        //public bool CanBook { get; set; }
    }

    public class PropertyMediaDto
    {
        public string Url { get; set; } = default!;
        public bool IsPrimary { get; set; }
        public EMediaType Type { get; set; }
    }

    public class PropertyLocationDto
    {
        public string Address { get; set; } = default!;
        public string City { get; set; } = default!;
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public bool IsVerified { get; set; }
    }

    public class LandlordDto
    {
        public string Id { get; set; } = default!;
        public string FirstName { get; set; } = default!;
        public bool IsVerified { get; set; }
    }
}
