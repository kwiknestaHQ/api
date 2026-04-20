namespace KwikNesta.Shared.ServiceDTOs.Payment
{
    public class PaystackInitRequest
    {
        public string Email { get; set; } = default!;
        public decimal Amount { get; set; }
        public string Reference { get; set; } = default!;
        public string Callback_url { get; set; } = default!;
    }
}