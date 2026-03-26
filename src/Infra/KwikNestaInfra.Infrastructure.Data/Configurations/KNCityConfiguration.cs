using KwikNestaInfra.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KwikNestaInfra.Infrastructure.Data.Configurations
{
    internal class KNCityConfiguration : IEntityTypeConfiguration<KNCity>
    {
        public void Configure(EntityTypeBuilder<KNCity> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.State)
                  .WithMany()
                  .HasForeignKey(x => x.StateId);
            builder.HasOne(x => x.Country)
                  .WithMany()
                  .HasForeignKey(x => x.CountryId);
            builder.Property(u => u.CreatedOn)
                   .IsRequired();
            builder.Property(u => u.Name)
                .HasMaxLength(200)
                .IsRequired();
            builder.Property(u => u.Longitude)
                   .IsRequired();
            builder.Property(u => u.Latitude)
                   .IsRequired();
        }
    }
}