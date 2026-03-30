using System.ComponentModel;

namespace KwikNesta.Shared.Models.Enumerations.Property
{
    public enum EDocumentType
    {
        [Description("Title Deed")]
        TitleDeed = 1,
        [Description("Utility Bill")]
        UtilityBill = 2,
        [Description("Government Id")]
        GovernmentId = 3,
        [Description("Purchase Receipt")]
        PurchaseReceipt = 4,
        [Description("Other")]
        Other = 99
    }
}