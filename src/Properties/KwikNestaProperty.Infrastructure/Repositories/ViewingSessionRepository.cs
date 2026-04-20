using KwikNesta.Shared.Implementations;
using KwikNestaProperty.Domain.Entities;
using KwikNestaProperty.Infrastructure.Contracts;
using KwikNestaProperty.Infrastructure.Data;

namespace KwikNestaProperty.Infrastructure.Repositories
{
    public class ViewingSessionRepository(PropertyServiceDbContext context) 
        : Repository<ViewingSession>(context), IViewingSessionRepository
    {
    }
}