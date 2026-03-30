using KwikNesta.Shared.Models.Enumerations.Property;
using KwikNestaProperty.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KwikNestaProperty.Infrastructure.Data.Configurations
{
    internal class KNPropertyConfiguration : IEntityTypeConfiguration<KNProperty>
    {
        public void Configure(EntityTypeBuilder<KNProperty> builder)
        {
            builder.ToTable("Properties");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Title)
                   .IsRequired()
                   .HasMaxLength(200);

            builder.Property(x => x.Description)
                   .HasMaxLength(2000);

            builder.Property(x => x.Price)
                   .HasPrecision(18, 2)
                   .IsRequired();

            builder.Property(x => x.Currency)
                   .IsRequired()
                   .HasMaxLength(3);

            builder.Property(x => x.Type)
                .HasConversion<string>()
                   .IsRequired();

            builder.Property(x => x.Status)
                .HasConversion<string>()
                   .IsRequired();

            builder.Property(x => x.ListingType)
                .HasConversion<string>()
                   .IsRequired();

            builder.Property(x => x.PriceFrequency)
                .HasConversion<string>()
                   .IsRequired();

            builder.Property(x => x.Bedrooms)
                   .IsRequired();

            builder.Property(x => x.Bathrooms)
                   .IsRequired();

            builder.Property(x => x.AreaSize)
                   .HasPrecision(10, 2);

            builder.Property(x => x.AreaUnit)
                   .IsRequired()
                   .HasMaxLength(10);

            builder.Property(x => x.ParkingSpaces);

            builder.Property(x => x.OwnerId)
               .IsRequired();

            builder.HasIndex(x => new { x.Type, x.Price })
                .HasFilter($"\"Status\" = '{EListingStatus.Available}'");

            builder.HasIndex(x => x.Price)
                   .HasFilter($"\"Status\" = '{EListingStatus.Available}'");

            builder.HasIndex(x => new { x.Type, x.Bedrooms, x.Bathrooms })
                   .HasFilter($"\"Status\" = '{EListingStatus.Available}'");
        }
    }
}