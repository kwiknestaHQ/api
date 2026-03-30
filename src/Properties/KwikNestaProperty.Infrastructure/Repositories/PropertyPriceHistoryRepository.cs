using KwikNestaProperty.Domain.Entities;
using KwikNestaProperty.Infrastructure.Contracts;
using KwikNestaProperty.Infrastructure.Data;

namespace KwikNestaProperty.Infrastructure.Repositories
{
    public class PropertyPriceHistoryRepository(PropertyServiceDbContext context)
        : IPropertyPriceHistoryRepository
    {
        private readonly PropertyServiceDbContext _context = context;

        public async Task LogAsync(Guid propertyId, decimal oldPrice, decimal newPrice)
        {
            await _context.PropertyPriceHistories.AddAsync(new PropertyPriceHistory
            {
                PropertyId = propertyId,
                OldPrice = oldPrice,
                NewPrice = newPrice
            });
        }

        public IQueryable<PropertyPriceHistory> Get(Guid propertyId)
        {
            return _context.PropertyPriceHistories
                .Where(p => p.PropertyId == propertyId)
                .OrderByDescending(p => p.CreatedOn);
        }
    }
}