using System.ComponentModel;

namespace KwikNesta.Shared.Models.Enumerations.Infra
{
    public enum EAgoraPlatform

    { 
        [Description("Other")]
        Other,
        [Description("Android")]
        Android,
        [Description("iOS")]
        IOs,
        [Description("Windows")]
        Windows = 5,
        [Description("Linux")]
        Linux,
        [Description("Web")]
        Web,
        [Description("macOS")]
        MacOs
    }
}