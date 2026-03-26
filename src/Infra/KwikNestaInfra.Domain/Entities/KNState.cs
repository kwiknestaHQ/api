using KwikNesta.Shared.Models;

namespace KwikNestaInfra.Domain.Entities
{
    public class KNState : BaseEntity
    {
        public string Name { get; set; } = default!;
        public Guid CountryId { get; set; }
        public KNCountry? Country { get; set; }
        public string CountryCode { get; set; } = default!;
        public string ISO2 { get; set; } = default!;
        public string Type { get; set; } = default!;
        public string Longitude { get; set; } = default!;
        public string Latitude { get; set; } = default!;
    }
}