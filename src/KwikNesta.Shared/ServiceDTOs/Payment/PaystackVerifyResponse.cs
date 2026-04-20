namespace KwikNesta.Shared.ServiceDTOs.Payment
{
    public class PaystackVerifyResponse
    {
        public bool Status { get; set; }
        public string Message { get; set; } = default!;
        public VerifyData Data { get; set; } = default!;
    }

    public class VerifyData
    {
        public string Reference { get; set; } = default!;
        public string Status { get; set; } = default!;
        public decimal Amount { get; set; } = default!;
        public string Currency { get; set; } = default!;
    }
}
