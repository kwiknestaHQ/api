using KwikNestaProperty.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KwikNestaProperty.Infrastructure.Data.Configurations
{
    internal class PropertyLocationConfiguration : IEntityTypeConfiguration<PropertyLocation>
    {
        public void Configure(EntityTypeBuilder<PropertyLocation> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Address)
                .HasMaxLength(500)
                .IsRequired();

            builder.Property(x => x.City)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(x => x.State)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(x => x.Country)
                .HasMaxLength(100)
                .IsRequired();

            //builder.Property(x => x.Coordinates)
            //    .HasColumnType("geography (point)")
            //    .IsRequired();

            builder.HasOne(x => x.Property)
                .WithOne(p => p.Location)
                .HasForeignKey<PropertyLocation>(x => x.PropertyId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(x => new { x.Latitude, x.Longitude })
               .HasFilter("\"IsVerified\" = true")
               .HasDatabaseName("IX_PropertyLocation_Lat_Lng_VerifiedOnly");

            //builder.HasIndex(x => x.Coordinates)
            //    .HasMethod("GIST");
        }
    }
}