namespace KwikNesta.Shared.ServiceDTOs.Payment
{
    public class PaystackRefundRequest
    {
        public string Transaction { get; set; } = default!;
        public decimal Amount { get; set; }
    }
}