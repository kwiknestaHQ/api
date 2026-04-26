namespace KwikNesta.Shared.ServiceDTOs.Payment
{
    public class InitiateTransferRequest
    {
        public string Source { get; set; } = "balance";
        public decimal Amount { get; set; }
        public string Recipient { get; set; } = default!;
        public string Reason { get; set; } = default!;
    }
}
