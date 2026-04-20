using KwikNestaProperty.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KwikNestaProperty.Infrastructure.Data.Configurations
{
    public class PropertyPriceHistoryConfiguration : IEntityTypeConfiguration<PropertyPriceHistory>
    {
        public void Configure(EntityTypeBuilder<PropertyPriceHistory> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.OldPrice)
                .HasPrecision(18, 2)
                .IsRequired();

            builder.Property(x => x.NewPrice)
               .HasPrecision(18, 2)
               .IsRequired();

            builder.HasOne(x => x.Property)
               .WithMany(x => x.PriceHistories)
               .HasForeignKey(x => x.PropertyId)
               .OnDelete(DeleteBehavior.Cascade);
        }
    }
}