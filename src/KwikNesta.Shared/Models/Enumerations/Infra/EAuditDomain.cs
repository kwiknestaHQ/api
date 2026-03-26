using System.ComponentModel;

namespace KwikNesta.Shared.Models.Enumerations.Infra
{
    public enum EAuditDomain
    {
        [Description("User")]
        User,
        [Description("User Account")]
        UserAccount,
        [Description("System Admin.")]
        SystemAdmin,
        [Description("Location")]
        Location
    }
}