using KwikNestaProperty.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KwikNestaProperty.Infrastructure.Data.Configurations
{
    public sealed class RentalIntentConfiguration : IEntityTypeConfiguration<RentalIntent>
    {
        public void Configure(EntityTypeBuilder<RentalIntent> builder)
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

            builder.Property(x => x.TotalAmountDue)
                .HasPrecision(18, 2)
                .IsRequired();

            builder.Property(x => x.ProposedMoveInDate)
                .IsRequired();

            builder.Property(x => x.LeaseDurationMonths)
                .IsRequired();

            builder.Property(x => x.Status)
                .HasConversion<string>()
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(x => x.DeclineReason)
                .HasMaxLength(1000);

            builder.Property(x => x.RespondedAt);

            builder.Property(x => x.ExpiresAt)
                .IsRequired();

            builder.HasOne(x => x.Property)
                .WithMany()
                .HasForeignKey(x => x.PropertyId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(x => new
            {
                x.TenantId,
                x.Status
            });

            builder.HasIndex(x => new
            {
                x.PropertyId,
                x.Status
            });

            builder.HasIndex(x => x.ExpiresAt);
        }
    }
}