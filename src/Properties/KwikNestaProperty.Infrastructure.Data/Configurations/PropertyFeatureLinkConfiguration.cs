using KwikNestaProperty.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KwikNestaProperty.Infrastructure.Data.Configurations
{
    internal class PropertyFeatureLinkConfiguration : IEntityTypeConfiguration<PropertyFeatureLink>
    {
        public void Configure(EntityTypeBuilder<PropertyFeatureLink> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.CustomFeature)
                   .HasMaxLength(200);

            builder.Property(x => x.CustomFeatureNormalized)
                   .HasMaxLength(200);

            builder.HasOne(x => x.Property)
                   .WithMany(p => p.PropertyFeatureLinks)
                   .HasForeignKey(x => x.PropertyId);

            builder.HasOne(x => x.Feature)
                   .WithMany()
                   .HasForeignKey(x => x.FeatureId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(x => x.PropertyId);
            builder.HasIndex(x => x.FeatureId);
            builder.HasIndex(x => x.CustomFeatureNormalized);
            builder.HasIndex(x => new { x.PropertyId, x.FeatureId })
               .IsUnique()
               .HasFilter("\"FeatureId\" IS NOT NULL");
            builder.HasIndex(x => new { x.PropertyId, x.CustomFeatureNormalized })
               .IsUnique()
               .HasFilter("\"CustomFeatureNormalized\" IS NOT NULL");
        }
    }
}