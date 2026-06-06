using System.ComponentModel;

namespace KwikNesta.Shared.Models.Enumerations.Payments
{
    public enum ESettlementOutcome
    {
        [Description("Full Settlement")]
        FullSettlement,
        [Description("Landlord No-Show")]
        LandlordNoShow,
        [Description("Tenant No-Show")]
        TenantNoShow,
        [Description("No-Show")]
        NoShow
    }
}