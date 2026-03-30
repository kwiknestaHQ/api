using System.ComponentModel;

namespace KwikNesta.Shared.Models.Enumerations.Property
{
    public enum EPriceFrequency
    {
        [Description("Total")]
        Total,
        [Description("Yearly")]
        PerYear,
        [Description("Monthly")]
        PerMonth,
        [Description("Per Night")]
        PerNight,
        [Description("Installment")]
        Installment,
        [Description("Negotiable")]
        Negotiable
    }
}