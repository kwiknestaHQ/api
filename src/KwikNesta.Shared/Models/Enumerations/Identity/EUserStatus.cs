using System.ComponentModel;

namespace KwikNesta.Shared.Models.Enumerations.Identity
{
    public enum EUserStatus
    {
        [Description("Pending Verification")]
        PendingVerification,
        [Description("Active")]
        Active,
        [Description("Deactivated")]
        Deactivated,
        [Description("Suspended")]
        Suspended
    }
}