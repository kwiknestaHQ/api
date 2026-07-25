using KwikNestaProperty.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KwikNestaProperty.Infrastructure.Data.Configurations
{
    public sealed class RentalPaymentConfiguration : IEntityTypeConfiguration<RentalPayment>
    {
        public void Configure(EntityTypeBuilder<RentalPayment> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.PropertyId)
                .IsRequired();

            builder.Property(x => x.TenantId)
                .IsRequired()
                .HasMaxLength(450);

            builder.Property(x => x.MonthlyRent)
                .HasPrecision(18, 2)
                .IsRequired();

            builder.Property(x => x.CautionDeposit)
                .HasPrecision(18, 2)
                .IsRequired();

            builder.Property(x => x.PlatformFee)
                .HasPrecision(18, 2)
                .IsRequired();

            builder.Property(x => x.TotalAmount)
                .HasPrecision(18, 2)
                .IsRequired();

            builder.Property(x => x.Status)
                .HasConversion<string>()
                .HasMaxLength(50)
                .IsRequired();

            builder.HasOne(x => x.Intent)
                .WithMany()
                .HasForeignKey(x => x.RentalIntentId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Property)
                .WithMany()
                .HasForeignKey(x => x.PropertyId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(x => new
            {
                x.RentalIntentId,
                x.Status
            });

            builder.HasIndex(x => x.PaymentReference)
                .IsUnique();

            builder.HasIndex(x => x.RentalIntentId)
                .IsUnique();
        }
    }
}