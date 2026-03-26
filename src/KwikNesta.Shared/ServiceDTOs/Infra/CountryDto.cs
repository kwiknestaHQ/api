namespace KwikNesta.Shared.ServiceDTOs.Infra
{
    public class CountryDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public string ISO2 { get; set; } = default!;
        public string ISO3 { get; set; } = default!;
        public string PhoneCode { get; set; } = default!;
        public string Currency { get; set; } = default!;
        public string CurrencyName { get; set; } = default!;
        public string CurrencySymbol { get; set; } = default!;
        public string TLD { get; set; } = default!;
        public string Longitude { get; set; } = default!;
        public string Latitude { get; set; } = default!;
        public string Emoji { get; set; } = default!;
        public string EmojiUnicode { get; set; } = default!;
        public bool IsActive { get; set; }
    }
}
