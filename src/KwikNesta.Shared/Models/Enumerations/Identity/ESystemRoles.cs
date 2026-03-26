using System.ComponentModel;

namespace KwikNesta.Shared.Models.Enumerations.Identity
{
    public enum ESystemRoles
    {
        None,
        [Description("SuperAdmin")]
        SuperAdmin,
        [Description("Admin")]
        Admin,
        [Description("LandLord")]
        LandLord,
        [Description("Tenant")]
        Tenant,
        [Description("Agent")]
        Agent
    }
}