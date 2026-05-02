using KwikNestaPayment.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KwikNestaPayment.Infrastructure.Data.Configurations
{
    public class RefundConfiguration : IEntityTypeConfiguration<KNRefund>
    {
        public void Configure(EntityTypeBuilder<KNRefund> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Reference)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(x => x.UserId)
                .IsRequired();

            builder.Property(x => x.ProviderReference)
               .HasMaxLength(100)
               .IsRequired();

            builder.Property(x => x.Status)
                .HasConversion<string>()
                .IsRequired();

            builder.Property(x => x.Reason)
               .HasMaxLength(500)
               .IsRequired(false);

            builder.Property(x => x.Amount)
               .HasPrecision(18, 2)
               .IsRequired();

            builder.Property(x => x.Provider)
               .HasConversion<string>()
               .IsRequired();

            builder.HasIndex(x => x.Reference)
                .IsUnique();
        }
    }
}