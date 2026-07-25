using System.ComponentModel;

namespace KwikNesta.Shared.Models.Enumerations.Property
{
    public enum ERentalIntentStatus
    {
        [Description("Pending")]
        Pending,
        [Description("Accepted By Landlord")]
        Accepted,
        [Description("Declined")]
        Declined,
        [Description("Expired")]
        Expired,
        [Description("Cancelled")]
        Cancelled
    }
}