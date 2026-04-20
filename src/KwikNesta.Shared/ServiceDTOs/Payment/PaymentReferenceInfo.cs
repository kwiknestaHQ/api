namespace KwikNesta.Shared.ServiceDTOs.Payment
{
    public class PaymentReferenceInfo
    {
        public DateTime TimestampUtc { get; set; }
        public int ServiceCode { get; set; }
        public string RandomPart { get; set; } = default!;
        public string CheckDigits { get; set; } = default!;
    }
}