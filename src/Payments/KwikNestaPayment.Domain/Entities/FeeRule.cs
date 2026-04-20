using KwikNesta.Shared.Models;
using KwikNesta.Shared.Models.Enumerations.Payments;

namespace KwikNestaPayment.Domain.Entities
{
    public class FeeRule : BaseEntity
    {
        public string Name { get; set; } = default!;
        public EFeeType Type { get; set; }
        public EFeeCalculationType CalculationType { get; set; }
        public decimal Value { get; set; }
        public EPaymentPurpose AppliesTo { get; set; }
        public bool IsActive { get; set; } = true;
        public int Priority { get; set; } = 1;
        public DateTime? StartsAt { get; set; }
        public DateTime? EndsAt { get; set; }

        public decimal? MinCap { get; set; }
        public decimal? MaxCap { get; set; }
    }
}