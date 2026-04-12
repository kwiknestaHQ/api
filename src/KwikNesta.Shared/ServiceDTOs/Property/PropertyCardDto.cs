namespace KwikNesta.Shared.ServiceDTOs.Property
{
    public class PropertyCardDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = default!;
        public decimal Price { get; set; }
        public int Bedrooms { get; set; }
        public string City { get; set; } = default!;
        public string? CoverImageUrl { get; set; }
        public double? DistanceKm { get; set; }
    }
}