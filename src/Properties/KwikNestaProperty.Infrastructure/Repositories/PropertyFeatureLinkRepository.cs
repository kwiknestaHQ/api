using KwikNesta.Shared.Implementations;
using KwikNestaProperty.Domain.Entities;
using KwikNestaProperty.Infrastructure.Contracts;
using KwikNestaProperty.Infrastructure.Data;

namespace KwikNestaProperty.Infrastructure.Repositories
{
    public class PropertyFeatureLinkRepository(PropertyServiceDbContext context) 
        : Repository<PropertyFeatureLink>(context), IPropertyFeatureLinkRepository
    {
    }
}