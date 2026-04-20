using KwikNesta.Shared.Models;
using KwikNesta.Shared.Models.Enumerations.Payments;

namespace KwikNestaPayment.Domain.Entities
{
    public class KNPayment : BaseEntity
    {
        public string Reference { get; set; } = default!;
        public decimal Amount { get; set; }
        public decimal PlatformFee { get; set; }
        public decimal NetAmount { get; set; }
        public string Currency { get; set; } = "NGN";
        public EPaymentStatus Status { get; set; } = EPaymentStatus.Pending;
        public EPaymentPurpose Purpose { get; set; }
        public Guid ReferenceId { get; set; }
        public DateTime? PaidAt { get; set; }
    }
}