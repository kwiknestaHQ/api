using KwikNesta.Shared.Implementations;
using KwikNestaProperty.Domain.Entities;
using KwikNestaProperty.Infrastructure.Contracts;
using KwikNestaProperty.Infrastructure.Data;

namespace KwikNestaProperty.Infrastructure.Repositories
{
    public class ViewingRequestRepository(PropertyServiceDbContext context) 
        : Repository<ViewingRequest>(context), IViewingRequestRepository
    {
    }
}