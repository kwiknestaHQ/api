using KwikNesta.Shared.Contracts;
using KwikNestaPayment.Domain.Entities;

namespace KwikNestaPayment.Infrastructure.Contracts
{
    public interface IPayoutAccountRepository : IRepository<KNPayoutAccount>
    {
    }
}