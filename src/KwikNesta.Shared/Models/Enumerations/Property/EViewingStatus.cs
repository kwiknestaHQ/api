using System.ComponentModel;

namespace KwikNesta.Shared.Models.Enumerations.Property
{
    public enum EViewingStatus
    {
        [Description("Pending")]
        Pending,
        [Description("Approved")]
        Approved,
        [Description("Rejected")]
        Rejected,
        [Description("Completed")]
        Completed,
        [Description("Cancelled")]
        Cancelled,
        [Description("No Show")]
        NoShow
    }
}