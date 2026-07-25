using KwikNestaProperty.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KwikNestaProperty.Infrastructure.Data.Configurations
{
    public sealed class TenancyAgreementConfiguration : IEntityTypeConfiguration<TenancyAgreement>
    {
        public void Configure(EntityTypeBuilder<TenancyAgreement> builder)
        {
            builder.HasKey(x => x.Id);

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

            builder.Property(x => x.MonthlyRent)
                .HasPrecision(18, 2)
                .IsRequired();

            builder.Property(x => x.PropertyTitle)
                .IsRequired();

            builder.Property(x => x.LeaseDurationMonths)
                .IsRequired();

            builder.Property(x => x.Status)
                .HasConversion<string>()
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(x => x.PropertyAddress)
                .IsRequired();

            builder.HasOne(x => x.Intent)
                .WithMany()
                .HasForeignKey(x => x.RentalIntentId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Payment)
                .WithMany()
                .HasForeignKey(x => x.RentalPaymentId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
