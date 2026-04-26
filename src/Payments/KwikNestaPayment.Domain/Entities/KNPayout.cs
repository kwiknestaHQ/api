using KwikNesta.Shared.Models;
using KwikNesta.Shared.Models.Enumerations.Payments;
using System.Transactions;

namespace KwikNestaPayment.Domain.Entities
{
    public class KNPayout : BaseEntity
    {
        public string Reference { get; set; } = default!;
        public decimal Amount { get; set; }
        public EPayoutStatus Status { get; set; }
        public EPaymentProvider Provider { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? CompletedAt { get; set; }
    }
}