using System.ComponentModel;

namespace KwikNesta.Shared.Models.Enumerations.Property
{
    public enum ETenancyAgreementStatus
    {
        [Description("Pending Document Generation")]
        PendingDocumentGeneration = 0,
        [Description("Awaiting Tenant Signature")]
        AwaitingTenantSignature = 1,
        [Description("Awaiting Landlord Signature")]
        AwaitingLandlordSignature = 2,
        [Description("Awaiting Signed Document")]
        AwaitingSignedDocument = 3,
        [Description("Fully Signed")]
        FullySigned = 4,
        [Description("Expired")]
        Expired = 5
    }
}