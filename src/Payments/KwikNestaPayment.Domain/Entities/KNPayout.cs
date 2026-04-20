using KwikNesta.Shared.Models;
using KwikNesta.Shared.Models.Enumerations.Payments;

namespace KwikNestaPayment.Domain.Entities
{
    public class KNPayout : BaseEntity
    {
        public string Reference { get; set; } = default!;
        public string TransferCode { get; set; } = default!;
        public decimal Amount { get; set; }
        public EPayoutStatus Status { get; set; }
        public string Recipient { get; set; } = default!;
        public DateTime? CompletedAt { get; set; }
    }
}