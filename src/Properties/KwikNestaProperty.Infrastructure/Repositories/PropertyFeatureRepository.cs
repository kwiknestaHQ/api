using KwikNesta.Shared.Implementations;
using KwikNestaProperty.Domain.Entities;
using KwikNestaProperty.Infrastructure.Contracts;
using Microsoft.EntityFrameworkCore;

namespace KwikNestaProperty.Infrastructure.Repositories
{
    public class PropertyFeatureRepository(DbContext context) 
        : Repository<PropertyFeature>(context), IPropertyFeatureRepository
    {
    }
}