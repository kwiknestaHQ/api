using KwikNesta.Shared.Models.Enumerations.Payments;
using KwikNestaPayment.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KwikNestaPayment.Infrastructure.Data.Configurations
{
    public class KNPaymentConfiguration : IEntityTypeConfiguration<KNPayment>
    {
        public void Configure(EntityTypeBuilder<KNPayment> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Status)
                   .IsRequired()
                   .HasConversion<string>();

            builder.Property(x => x.Purpose)
                   .IsRequired()
                   .HasConversion<string>();

            builder.Property(x => x.Provider)
                   .IsRequired()
                   .HasDefaultValue(EPaymentProvider.Paystack)
                   .HasConversion<string>();

            builder.Property(x => x.Reference)
                   .HasMaxLength(100);

            builder.Property(x => x.Amount)
                   .HasPrecision(18, 2)
                   .IsRequired();

            builder.Property(x => x.NetAmount)
                   .HasPrecision(18, 2)
                   .IsRequired();

            builder.Property(x => x.Currency)
                   .IsRequired()
                   .HasMaxLength(3);

            builder.HasIndex(x => x.Reference)
                  .IsUnique();

            builder.Property(x => x.ReferenceId)
                   .IsRequired();
        }
    }
}