using System.ComponentModel;

namespace KwikNesta.Shared.Models.Enumerations.Infra
{
    public enum EAuditAction
    {
        [Description("Logged In")]
        Login,
        [Description("Changed Password")]
        ChangedPassword,
        [Description("Deactivated Account")]
        DeactivatedAccount,
        [Description("Suspended Account")]
        SuspendedAccount,
        [Description("Restored Account")]
        RestoredAccount,
        [Description("Updated User Details")]
        UpdatedUserDetails = 1000,

        [Description("Migrated Location Data")]
        MigratedLocationData,
        [Description("Toggled Location Active Status")]
        LocationToggle
    }
}