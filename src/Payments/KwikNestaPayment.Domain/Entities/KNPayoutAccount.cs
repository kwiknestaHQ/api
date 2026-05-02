using KwikNesta.Shared.Models;

namespace KwikNestaPayment.Domain.Entities
{
    public class KNPayoutAccount : BaseEntity
    {
        public string UserId { get; set; } = default!;
        public string RecipientCode { get; set; } = default!;
        public string AccountNumber { get; set; } = default!;
        public string BankCode { get; set; } = default!;
        public string BankName { get; set; } = default!;
        public string AccountName { get; set; } = default!;
        public bool IsActive { get; set; } = true;
    }
}