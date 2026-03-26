using KwikNesta.Shared.Models.Enumerations.Property;

namespace KwikNestaProperty.Infrastructure.Data.Configurations.DbSeeder
{
    public static class FeatureSeed
    {
        public static readonly List<(Guid Id, string Name, EFeatureCategory Category)> FeaturesToSeed = new()
        {
            (Guid.Parse("9BED23D9-8F16-4444-83D3-9A114A8E42F3"), "Air Conditioning", EFeatureCategory.Interior),
            (Guid.Parse("8BED23D9-8F16-4444-83D3-9A114A8E42F4"), "Furnished", EFeatureCategory.Interior),
            (Guid.Parse("7BED23D9-8F16-4444-83D3-9A114A8E42F5"), "Semi-Furnished", EFeatureCategory.Interior),
            (Guid.Parse("6BED23D9-8F16-4444-83D3-9A114A8E42F6"), "Built-in Wardrobes", EFeatureCategory.Interior),
            (Guid.Parse("5BED23D9-8F16-4444-83D3-9A114A8E42F7"), "Smart Home System", EFeatureCategory.Interior),
            (Guid.Parse("4BED23D9-8F16-4444-83D3-9A114A8E42F8"), "Laundry Room", EFeatureCategory.Interior),

            (Guid.Parse("3BED23D9-8F16-4444-83D3-9A114A8E42F9"), "Balcony", EFeatureCategory.Exterior),
            (Guid.Parse("2BED23D9-8F16-4444-83D3-9A114A8E42F0"), "Terrace", EFeatureCategory.Exterior),
            (Guid.Parse("1BED23D9-8F16-4444-83D3-9A114A8E42F1"), "Garden", EFeatureCategory.Exterior),
            (Guid.Parse("0BED23D9-8F16-4444-83D3-9A114A8E42F2"), "Fence", EFeatureCategory.Exterior),
            (Guid.Parse("ABED23D9-8F16-4444-83D3-9A114A8E42FA"), "Gated Compound", EFeatureCategory.Exterior),

            (Guid.Parse("BBED23D9-8F16-4444-83D3-9A114A8E42FB"), "CCTV", EFeatureCategory.Security),
            (Guid.Parse("CBED23D9-8F16-4444-83D3-9A114A8E42FB"), "Security Doors", EFeatureCategory.Security),
            (Guid.Parse("DBED23D9-8F16-4444-83D3-9A114A8E42FC"), "Burglar Alarm", EFeatureCategory.Security),
            (Guid.Parse("EBED23D9-8F16-4444-83D3-9A114A8E42FD"), "Gated Estate", EFeatureCategory.Security),
            (Guid.Parse("ECED23D9-8F16-4444-83D3-9A114A8E42FE"), "Security", EFeatureCategory.Security),

            (Guid.Parse("EDED23D9-8F16-4444-83D3-9A114A8E420E"), "Electricity", EFeatureCategory.Utilities),
            (Guid.Parse("EEED23D9-8F16-4444-83D3-9A114A8E421E"), "Prepaid Meter", EFeatureCategory.Utilities),
            (Guid.Parse("E0ED23D9-8F16-4444-83D3-9A114A8E422E"), "Boys Quarters", EFeatureCategory.Utilities),
            (Guid.Parse("E1ED23D9-8F16-4444-83D3-9A114A8E423E"), "Generator", EFeatureCategory.Utilities),
            (Guid.Parse("E2ED23D9-8F16-4444-83D3-9A114A8E424E"), "Inverter", EFeatureCategory.Utilities),
            (Guid.Parse("E3ED23D9-8F16-4444-83D3-9A114A8E425E"), "Borehole", EFeatureCategory.Utilities),
            (Guid.Parse("E4ED23D9-8F16-4444-83D3-9A114A8E426E"), "Water Suply", EFeatureCategory.Utilities),
            (Guid.Parse("E5ED23D9-8F16-4444-83D3-9A114A8E427E"), "Cable TV", EFeatureCategory.Utilities),
            (Guid.Parse("E6ED23D9-8F16-4444-83D3-9A114A8E428E"), "Internet", EFeatureCategory.Utilities),
            (Guid.Parse("E7ED23D9-8F16-4444-83D3-9A114A8E429E"), "Parking Space", EFeatureCategory.Utilities),
            (Guid.Parse("E8ED23D9-8F16-4444-83D3-9A114A8E4200"), "Swimming Pool", EFeatureCategory.Utilities),
            (Guid.Parse("E9ED23D9-8F16-4444-83D3-9A114A8E4201"), "Gym", EFeatureCategory.Utilities),
            (Guid.Parse("00ED23D9-8F16-4444-83D3-9A114A8E4202"), "Jacuzzi", EFeatureCategory.Utilities),
            (Guid.Parse("01ED23D9-8F16-4444-83D3-9A114A8E4203"), "Garage", EFeatureCategory.Utilities),
            (Guid.Parse("02ED23D9-8F16-4444-83D3-9A114A8E4204"), "Elevator", EFeatureCategory.Utilities)
        };
    }
}