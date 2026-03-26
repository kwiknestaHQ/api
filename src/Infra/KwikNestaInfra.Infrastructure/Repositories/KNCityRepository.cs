using KwikNesta.Shared.Implementations;
using KwikNestaInfra.Domain.Entities;
using KwikNestaInfra.Infrastructure.Contracts;
using KwikNestaInfra.Infrastructure.Data;

namespace KwikNestaInfra.Infrastructure.Repositories
{
    public class KNCityRepository(InfraServiceDbContext context) 
        : Repository<KNCity>(context), IKNCityRepository
    {
    }
}