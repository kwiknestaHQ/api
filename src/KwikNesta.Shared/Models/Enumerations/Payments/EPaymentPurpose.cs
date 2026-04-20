using System.ComponentModel;

namespace KwikNesta.Shared.Models.Enumerations.Payments
{
    public enum EPaymentPurpose
    {
        [Description("View Request")]
        Viewing,
        [Description("Rent")]
        Rent
    }
}
