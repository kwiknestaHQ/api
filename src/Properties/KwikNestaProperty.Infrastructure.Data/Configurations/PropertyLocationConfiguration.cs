using KwikNesta.Shared.Models.Enumerations.Property;
using KwikNestaProperty.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KwikNestaProperty.Infrastructure.Data.Configurations
{
    public class PropertyLocationConfiguration : IEntityTypeConfiguration<PropertyLocation>
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

            builder.Property(x => x.PostalCode)
                .HasMaxLength(20)
                .IsRequired(false);

            builder.Property(x => x.VerificationStatus)
                .HasConversion<string>()
                .IsRequired();

            builder.HasOne(x => x.Property)
                .WithOne(p => p.Location)
                .HasForeignKey<PropertyLocation>(x => x.PropertyId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(x => new { x.Latitude, x.Longitude })
               .HasFilter($"\"VerificationStatus\" = '{ELocationVerificationStatus.Verified}'")
               .HasDatabaseName("IX_PropertyLocation_Lat_Lng_VerifiedOnly");
        }
    }
}