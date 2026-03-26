using KwikNesta.Shared.Implementations;
using KwikNestaInfra.Domain.Entities;
using KwikNestaInfra.Infrastructure.Contracts;
using KwikNestaInfra.Infrastructure.Data;

namespace KwikNestaInfra.Infrastructure.Repositories
{
    public class KNStateRepository(InfraServiceDbContext context)
        : Repository<KNState>(context), IKNStateRepository
    {
    }
}