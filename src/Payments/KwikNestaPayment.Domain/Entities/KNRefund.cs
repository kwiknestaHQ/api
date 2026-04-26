namespace KwikNestaPayment.Domain.Entities
{
    public class KNRefund : KNPayout
    {
        public string ProviderReference { get; set; } = default!;
        public string PaymentReference { get; set; } = default!;
        public DateTime? ExpectedAt { get; set; }
        public string? Reason { get; set; }
    }
}
