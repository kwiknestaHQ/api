namespace KwikNesta.Shared.ServiceDTOs.Payment
{
    public class ViewRequestPaymentInitResult
    {
        public string AuthorizationUrl { get; set; } = default!;
        public string PaymentCode { get; set; } = default!;
        public string Reference { get; set; } = default!;
    }
}