using KwikNesta.Shared.Models.Enumerations.Property;

namespace KwikNesta.Shared.ServiceCommands.Property
{
    public abstract class BasePropertyRequest
    {
        public string Title { get; set; } = default!;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public EPropertyType Type { get; set; }
        public EListingType ListingType { get; set; }
        public EPriceFrequency PriceFrequency { get; set; }
        public int Bedrooms { get; set; }
        public int Bathrooms { get; set; }
        public double AreaSize { get; set; }
        public int? ParkingSpaces { get; set; }
    }
}