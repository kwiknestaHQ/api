using KwikNesta.Shared.Implementations;
using KwikNestaInfra.Domain.Entities;
using KwikNestaInfra.Infrastructure.Contracts;
using KwikNestaInfra.Infrastructure.Data;

namespace KwikNestaInfra.Infrastructure.Repositories
{
    public class KNTimeZoneRepository(InfraServiceDbContext context)
        : Repository<KNTimeZone>(context), IKNTimeZoneRepository
    {
    }
}