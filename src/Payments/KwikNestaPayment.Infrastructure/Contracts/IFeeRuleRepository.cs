using KwikNesta.Shared.Contracts;
using KwikNesta.Shared.Models.Enumerations.Payments;
using KwikNestaPayment.Domain.Entities;

namespace KwikNestaPayment.Infrastructure.Contracts
{
    public interface IFeeRuleRepository : IRepository<FeeRule>
    {
        Task<List<FeeRule>> GetFeeRules(EPaymentPurpose appliesTo);
    }
}