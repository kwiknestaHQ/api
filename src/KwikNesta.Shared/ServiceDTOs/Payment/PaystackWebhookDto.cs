namespace KwikNesta.Shared.ServiceDTOs.Payment
{
    public class PaystackWebhookDto
    {
        public string Event { get; set; } = default!;
        public PaystackData Data { get; set; } = default!;
    }

    public class PaystackData
    {
        public long Id { get; set; }
        public string Reference { get; set; } = default!;
        public string Status { get; set; } = default!;
        public int Amount { get; set; }
        public string Currency { get; set; } = default!;
        public string Transfer_code { get; set; } = default!;
    }
}