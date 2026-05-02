using KwikNestaPayment.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KwikNestaPayment.Infrastructure.Data.Configurations
{
    public class PayoutAccountConfiguration : IEntityTypeConfiguration<KNPayoutAccount>
    {
        public void Configure(EntityTypeBuilder<KNPayoutAccount> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.UserId)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(x => x.RecipientCode)
               .HasMaxLength(100)
               .IsRequired();

            builder.Property(x => x.AccountNumber)
               .HasMaxLength(30)
               .IsRequired();

            builder.Property(x => x.BankCode)
                .HasMaxLength(30)
                .IsRequired();

            builder.Property(x => x.BankName)
               .HasMaxLength(100)
               .IsRequired();

            builder.Property(x => x.AccountName)
               .HasMaxLength(500)
               .IsRequired();

            builder.Property(x => x.IsActive)
               .HasDefaultValue(true);

            builder.HasIndex(x => new { x.BankCode, x.AccountNumber })
                .IsUnique();
        }
    }
}