namespace KwikNesta.Shared.ServiceDTOs.Payment
{
    public class PaystackCreateTransferRequest
    {
        public string Type { get; set; } = default!;
        public string Name { get; set; } = default!;
        public string Currency { get; set; } = default!;
        public string Account_number { get; set; } = default!;
        public string Bank_code { get; set; } = default!;
    }
}