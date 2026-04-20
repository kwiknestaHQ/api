namespace KwikNesta.Shared.ServiceDTOs.Payment
{
    public class FeeResult
    {
        public decimal Charge { get; set; }
        public decimal PlatformFee { get; set; }
        public decimal Discount { get; set; }
        public decimal FinalAmount { get; set; }
    }
}