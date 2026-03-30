using KwikNesta.Shared.Models;
using KwikNesta.Shared.Models.Enumerations.Property;

namespace KwikNestaProperty.Domain.Entities
{
    public class PropertyLocation : BaseEntity
    {
        public Guid PropertyId { get; set; }
        public KNProperty Property { get; set; } = default!;

        public string Address { get; set; } = default!;
        public string City { get; set; } = default!;
        public string State { get; set; } = default!;
        public string Country { get; set; } = default!;
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public string PostalCode { get; set; } = default!;
        public ELocationVerificationStatus VerificationStatus { get; set; } = ELocationVerificationStatus.Pending;
    }
}
