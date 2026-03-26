using KwikNesta.Shared.Models;

namespace KwikNestaInfra.Domain.Entities
{
    public class KNCountry : BaseEntity
    {
        public string Name { get; set; } = default!;
        public string ISO2 { get; set; } = default!;
        public string ISO3 { get; set; } = default!;
        public string NumericCode { get; set; } = default!;
        public string PhoneCode { get; set; } = default!;
        public string Capital { get; set; } = default!;
        public string Currency { get; set; } = default!;
        public string CurrencyName { get; set; } = default!;
        public string CurrencySymbol { get; set; } = default!;
        public string TLD { get; set; } = default!;
        public string Region { get; set; } = default!;
        public int? RegionId { get; set; }
        public string SubRegion { get; set; } = default!;
        public int? SubRegionId { get; set; }
        public string Native { get; set; } = default!;
        public string Nationality { get; set; } = default!;
        public string Longitude { get; set; } = default!;
        public string Latitude { get; set; } = default!;
        public string Emoji { get; set; } = default!;
        public string EmojiUnicode { get; set; } = default!;
        public bool IsActive { get; set; }
    }
}