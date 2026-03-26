using KwikNestaInfra.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KwikNestaInfra.Infrastructure.Data.Configurations
{
    internal class KNCountryConfiguration : IEntityTypeConfiguration<KNCountry>
    {
        public void Configure(EntityTypeBuilder<KNCountry> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(u => u.CreatedOn)
                   .IsRequired();
            builder.Property(u => u.Longitude)
                   .IsRequired();
            builder.Property(u => u.Latitude)
                   .IsRequired();
            builder.Property(u => u.Name)
                .HasMaxLength(200)
                .IsRequired();
            builder.Property(u => u.Nationality)
                .IsRequired();
            builder.HasIndex(x => x.ISO2);
        }
    }
}
