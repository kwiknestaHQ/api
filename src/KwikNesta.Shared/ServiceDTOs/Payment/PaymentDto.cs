using KwikNesta.Shared.Models.Enumerations.Payments;

namespace KwikNesta.Shared.ServiceDTOs.Payment
{
    public class PaymentDto
    {
        public Guid Id { get; set; }
        public string UserId { get; set; } = default!;
        public string Reference { get; set; } = default!;
        public decimal Amount { get; set; }
        public decimal PlatformFee { get; set; }
        public decimal NetAmount { get; set; }
        public string Currency { get; set; } = default!;
        public string PaymentCode { get; set; } = default!;
        public EPaymentStatus Status { get; set; }
        public EPaymentPurpose Purpose { get; set; }
        public Guid ReferenceId { get; set; }
        public DateTime? PaidAt { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
