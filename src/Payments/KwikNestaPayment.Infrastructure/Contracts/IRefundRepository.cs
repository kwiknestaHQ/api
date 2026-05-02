using KwikNesta.Shared.Contracts;
using KwikNestaPayment.Domain.Entities;

namespace KwikNestaPayment.Infrastructure.Contracts
{
    public interface IRefundRepository : IRepository<KNRefund>
    {
    }
}