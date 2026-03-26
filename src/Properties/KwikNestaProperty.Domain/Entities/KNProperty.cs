using KwikNesta.Shared.Models;
using KwikNesta.Shared.Models.Enumerations.Property;

namespace KwikNestaProperty.Domain.Entities
{
    public class KNProperty : BaseEntity
    {
        public string Title { get; set; } = default!;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public string Currency { get; set; } = default!;
        public EPropertyType Type { get; set; }
        public EListingStatus Status { get; set; } = EListingStatus.Draft;
        public bool IsOwnerShipVerified { get; set; }
        public string? StatusReason { get; set; }

        public int Bedrooms { get; set; }
        public int Bathrooms { get; set; }
        public double AreaSize { get; set; }
        public string AreaUnit { get; set; } = "sqm";
        public int? ParkingSpaces { get; set; }

        // Relationships
        public string OwnerId { get; set; } = default!;

        public PropertyLocation Location { get; set; } = default!;
        public ICollection<PropertyFeatureLink> PropertyFeatureLinks { get; set; } = [];
        public ICollection<PropertyMedia> Media { get; set; } = [];
        public ICollection<ViewingRequest> ViewingRequests { get; set; } = [];
        public ICollection<OwnershipVerificationRequest> OwnershipVerificationRequests { get; set; } = [];
    }
}