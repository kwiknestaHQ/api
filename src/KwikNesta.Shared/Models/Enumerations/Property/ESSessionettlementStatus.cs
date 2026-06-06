using System.ComponentModel;

namespace KwikNesta.Shared.Models.Enumerations.Property
{
    public enum ESSessionettlementStatus
    {
        [Description("Pending")]
        Pending,
        [Description("Scheduled")]
        Scheduled,
        [Description("Completed")]
        Completed
    }
}