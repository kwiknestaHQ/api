using System.ComponentModel;

namespace KwikNesta.Shared.Models.Enumerations.Property
{
    public enum ERentalPaymentStatus
    {
        [Description("Pending")]
        Pending,
        [Description("Paid")]
        Paid,
        [Description("InEscrow")]
        InEscrow,
        [Description("Disbursed")]
        Disbursed,
        [Description("Refunded")]
        Refunded,
        [Description("Failed")]
        Failed
    }
}