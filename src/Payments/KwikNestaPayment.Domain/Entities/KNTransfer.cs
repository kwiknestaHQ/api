namespace KwikNestaPayment.Domain.Entities
{
    public class KNTransfer : KNPayout
    {
        public string TransferCode { get; set; } = default!;
        public string Recipient { get; set; } = default!;
        public string? Reason { get; set; }
    }
}
