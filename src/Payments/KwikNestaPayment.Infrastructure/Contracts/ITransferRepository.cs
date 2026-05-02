using KwikNesta.Shared.Contracts;
using KwikNestaPayment.Domain.Entities;

namespace KwikNestaPayment.Infrastructure.Contracts
{
    public interface ITransferRepository : IRepository<KNTransfer>
    {
    }
}