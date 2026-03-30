using KwikNesta.Shared.Models.Enumerations.Property;

namespace KwikNesta.Shared.Helpers
{
    public static class ListingRules
    {
        public static readonly Dictionary<EListingType, EPriceFrequency[]> AllowedFrequencies =
        new()
        {
                { EListingType.Sale, new[] { EPriceFrequency.Total } },
                { EListingType.Rent, new[] { EPriceFrequency.PerYear, EPriceFrequency.PerMonth } },
                { EListingType.ShortLet, new[] { EPriceFrequency.PerNight } },
                { EListingType.Lease, new[] { EPriceFrequency.PerYear } },
                { EListingType.OffPlan, new[] { EPriceFrequency.Installment } },
                { EListingType.JointVenture, new[] { EPriceFrequency.Negotiable } }
        };
    }
}
