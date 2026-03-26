using System.ComponentModel;

namespace KwikNesta.Shared.Models.Enumerations.Property
{
    public enum EFeatureCategory
    {
        [Description("Interior")]
        Interior,
        [Description("Exterior")]
        Exterior,
        [Description("Security")]
        Security,
        [Description("Utilities & Infrastructures")]
        Utilities
    }
}