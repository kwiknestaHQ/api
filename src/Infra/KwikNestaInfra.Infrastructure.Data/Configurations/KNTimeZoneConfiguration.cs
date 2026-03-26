using KwikNestaInfra.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KwikNestaInfra.Infrastructure.Data.Configurations
{
    internal class KNTimeZoneConfiguration : IEntityTypeConfiguration<KNTimeZone>
    {
        public void Configure(EntityTypeBuilder<KNTimeZone> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.Country)
                  .WithMany()
                  .HasForeignKey(x => x.CountryId);
            builder.Property(u => u.CreatedOn)
                   .IsRequired();
            builder.Property(u => u.TZName)
                   .HasMaxLength(200)
                   .IsRequired();
            builder.Property(u => u.ZoneName)
                .HasMaxLength(200)
                   .IsRequired();
        }
    }
}