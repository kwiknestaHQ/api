using KwikNesta.Shared.Models;
using KwikNesta.Shared.Models.Enumerations.Payments;

namespace KwikNestaPayment.Domain.Entities
{
    public class KNSettlement : BaseEntity
    {
        public string BeneficiaryId { get; set; } = default!;
        public decimal Amount { get; set; }
        public EPaymentPurpose Purpose { get; set; }
        public Guid ReferenceId { get; set; }
        public ESettlementStatus Status { get; set; } = ESettlementStatus.Pending;
        public DateTime? SettledAt { get; set; }
    }
}