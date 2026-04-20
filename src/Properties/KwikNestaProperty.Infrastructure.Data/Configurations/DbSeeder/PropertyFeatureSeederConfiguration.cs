using KwikNestaProperty.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KwikNestaProperty.Infrastructure.Data.Configurations.DbSeeder
{
    public class PropertyFeatureSeederConfiguration : IEntityTypeConfiguration<PropertyFeature>
    {
        public void Configure(EntityTypeBuilder<PropertyFeature> builder)
        {
            var features = FeatureSeed.FeaturesToSeed
                .Select(f => new PropertyFeature
                {
                    Id = f.Id,
                    Name = f.Name,
                    Category = f.Category,
                    NameNormalized = f.Name.ToLowerInvariant()
                })
                .ToArray();

            builder.HasData(features);
        }
    }
}