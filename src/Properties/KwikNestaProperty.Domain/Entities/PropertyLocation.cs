using KwikNesta.Shared.Models;

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
        public long Longitude { get; set; }
        public long Latitude { get; set; }

        //public Point Coordinates { get; set; } = default!;
        public bool IsVerified { get; set; }
    }
}
