namespace KwikNesta.Shared.ServiceDTOs.Property
{
    public class ViewRequestPaymentInitResult
    {
        public string AuthorizationUrl { get; set; } = default!;
        public string PaymentCode { get; set; } = default!;
        public string Reference { get; set; } = default!;
    }
}