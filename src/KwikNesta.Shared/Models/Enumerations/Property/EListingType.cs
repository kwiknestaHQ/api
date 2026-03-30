using System.ComponentModel;

namespace KwikNesta.Shared.Models.Enumerations.Property
{
    public enum EListingType
    {
        [Description("Sale")]
        Sale = 1,
        [Description("Rent")]
        Rent = 2,
        [Description("Short Let")]
        ShortLet = 3,
        [Description("Lease")]
        Lease = 4,
        [Description("Joint Venture")]
        JointVenture = 5,
        [Description("Off Plan")]
        OffPlan = 6
    }
}
