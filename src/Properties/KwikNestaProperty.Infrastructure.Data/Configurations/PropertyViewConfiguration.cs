using KwikNestaProperty.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KwikNestaProperty.Infrastructure.Data.Configurations
{
    internal class PropertyViewConfiguration : IEntityTypeConfiguration<PropertyView>
    {
        public void Configure(EntityTypeBuilder<PropertyView> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.ViewedAt)
                .IsRequired();

            builder.HasOne(x => x.Property)
               .WithMany(x => x.PropertyViews)
               .HasForeignKey(x => x.PropertyId)
               .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
