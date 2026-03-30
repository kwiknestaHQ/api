using KwikNestaProperty.Domain.Entities;

namespace KwikNestaProperty.Infrastructure.Contracts
{
    public interface IPropertyPriceHistoryRepository
    {
        IQueryable<PropertyPriceHistory> Get(Guid propertyId);
        Task LogAsync(Guid propertyId, decimal oldPrice, decimal newPrice);
    }
}