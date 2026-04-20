namespace KwikNesta.Shared.Models.Enumerations.Payments
{
    public enum ESettlementStatus
    {
        Pending,
        Processing,
        Completed, 
        Failed,
        Reversed,
        Held,
        Cancelled,
    }
}