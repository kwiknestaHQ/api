using System.ComponentModel;

namespace KwikNesta.Shared.Models.Enumerations.Property
{
    public enum ELocationVerificationStatus
    {
        [Description("Pending")]
        Pending,
        [Description("Verified")]
        Verified,
        [Description("Failed")]
        Failed,
        [Description("Requires Reverification")]
        RequiresReverification
    }
}