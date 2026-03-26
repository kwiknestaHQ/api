using KwikNesta.Shared.Contracts;
using KwikNestaInfra.Domain.Entities;

namespace KwikNestaInfra.Infrastructure.Contracts
{
    public interface IAuditLogRepository : IRepository<AuditLog>
    {
    }
}