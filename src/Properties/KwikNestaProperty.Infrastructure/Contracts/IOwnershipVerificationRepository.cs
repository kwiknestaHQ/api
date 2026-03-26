using KwikNesta.Shared.Contracts;
using KwikNestaProperty.Domain.Entities;

namespace KwikNestaProperty.Infrastructure.Contracts
{
    public interface IOwnershipVerificationRepository : IRepository<OwnershipVerificationRequest>
    {
    }
}