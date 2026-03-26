using KwikNestaInfra.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KwikNestaInfra.Infrastructure.Data.Configurations
{
    internal class KNStateConfiguration : IEntityTypeConfiguration<KNState>
    {
        public void Configure(EntityTypeBuilder<KNState> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.Country)
                  .WithMany()
                  .HasForeignKey(x => x.CountryId);
            builder.Property(u => u.CreatedOn)
                   .IsRequired();
            builder.Property(u => u.Longitude)
                   .IsRequired();
            builder.Property(u => u.Latitude)
                   .IsRequired();
            builder.Property(u => u.Name)
                .HasMaxLength(200)
                .IsRequired();
            builder.HasIndex(x => new { x.ISO2, x.CountryId });
        }
    }
}