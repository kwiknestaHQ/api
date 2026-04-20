using System.Text.Json.Serialization;

namespace KwikNesta.Shared.ServiceDTOs.Property
{
    public class PropertyLeanDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = default!;
        public string Address { get; set; } = default!;
        public string Type { get; set; } = default!;
        public string ListingType { get; set; } = default!;
        public string PriceFrequency { get; set; } = default!;
        public string City { get; set; } = default!;
        public string State { get; set; } = default!;
        public string Country { get; set; } = default!;
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string LocationVerificationStatus { get; set; } = default!;
        [JsonIgnore]
        public string OwnerId { get; set; } = default!;
    }

    public class PropertyVeryLeanDto
    {
        public Guid Id { get; set; }
        public string OwnerId { get; set; } = default!;
        public string Title { get; set; } = default!;
        public string Address { get; set; } = default!;
        public decimal Price { get; set; }
        public string OwnerFirstName { get; set; } = default!;
        public string OwnerEmail { get; set; } = default!;
    }
}