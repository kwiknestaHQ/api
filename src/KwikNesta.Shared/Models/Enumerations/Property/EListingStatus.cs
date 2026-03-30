using System.ComponentModel;

namespace KwikNesta.Shared.Models.Enumerations.Property
{
    public enum EListingStatus
    {
        [Description("Draft")]
        Draft,
        [Description("Pending")]
        Pending,
        [Description("Available")]
        Available,
        [Description("Pending Payment ")]
        PendingPayment,
        [Description("Sold")]
        Sold,
        [Description("Rented")]
        Rented,
        [Description("Withdrawn")]
        Withdrawn,
        [Description("Archived")]
        Archived,
        [Description("Verification Failed")]
        VerificationFailed
    }
}