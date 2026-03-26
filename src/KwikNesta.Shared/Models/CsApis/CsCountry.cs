namespace KwikNesta.Shared.Models.CsApis
{
    public class CsCountry
    {
        public long Id { get; set; }
        public string Name { get; set; } = default!;
        public string ISO2 { get; set; } = default!;
        public string ISO3 { get; set; } = default!;
        public string Numeric_Code { get; set; } = default!;
        public string PhoneCode { get; set; } = default!;
        public string Capital { get; set; } = default!;
        public string Currency { get; set; } = default!;
        public string Currency_Name { get; set; } = default!;
        public string Currency_Symbol { get; set; } = default!;
        public string TLD { get; set; } = default!;
        public string Region { get; set; } = default!;
        public int? Region_Id { get; set; }
        public string SubRegion { get; set; } = default!;
        public int? Subregion_Id { get; set; }
        public string Native { get; set; } = default!;
        public string Nationality { get; set; } = default!;
        public string Longitude { get; set; } = default!;
        public string Latitude { get; set; } = default!;
        public string Emoji { get; set; } = default!;
        public string EmojiU { get; set; } = default!;
        public string TimeZones { get; set; } = default!;
        public string? Translations { get; set; } = default!;
    }
}