using KwikNesta.Shared.Implementations;
using KwikNestaProperty.Domain.Entities;
using KwikNestaProperty.Infrastructure.Contracts;
using KwikNestaProperty.Infrastructure.Data;

namespace KwikNestaProperty.Infrastructure.Repositories
{
    public class PropertyViewRepository(PropertyServiceDbContext context) 
        : Repository<PropertyView>(context), IPropertyViewRepository
    {
    }
}