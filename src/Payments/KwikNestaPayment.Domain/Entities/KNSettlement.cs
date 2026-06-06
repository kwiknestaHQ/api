using KwikNesta.Shared.Models;
using KwikNesta.Shared.Models.Enumerations.Payments;

namespace KwikNestaPayment.Domain.Entities
{
    public class KNSettlement : BaseEntity
    {
        public string BeneficiaryId { get; set; } = default!;
        public decimal Amount { get; set; }
        public EPaymentPurpose Purpose { get; set; }
        public EPayoutType Type { get; set; }
        public Guid ReferenceId { get; set; }
        public ESettlementStatus Status { get; set; } = ESettlementStatus.Pending;
        public DateTime? SettledAt { get; set; }
        public DateTime StartAfter { get; set; } = DateTime.UtcNow.AddHours(12);

        public void MarkAsSettled()
        {
            SettledAt = DateTime.UtcNow;
            Status = ESettlementStatus.Completed;
            LastUpdatedOn = DateTime.UtcNow;
        }

        public void MarkAsFailed()
        {
            SettledAt = null;
            Status = ESettlementStatus.Failed;
            LastUpdatedOn = DateTime.UtcNow;
        }
    }
}