using System.ComponentModel;

namespace KwikNesta.Shared.Models.Enumerations.Infra
{
    public enum EActionInitiator
    {
        [Description("User")]
        User,
        [Description("Admin")]
        Admin,
        [Description("System")]
        System
    }
}