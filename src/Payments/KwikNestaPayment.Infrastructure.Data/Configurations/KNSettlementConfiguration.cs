using KwikNestaPayment.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KwikNestaPayment.Infrastructure.Data.Configurations
{
    public class KNSettlementConfiguration : IEntityTypeConfiguration<KNSettlement>
    {
        public void Configure(EntityTypeBuilder<KNSettlement> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Status)
                   .IsRequired()
                   .HasConversion<string>();

            builder.Property(x => x.Purpose)
                   .IsRequired()
                   .HasConversion<string>();

            builder.Property(x => x.Amount)
                   .HasPrecision(18, 2)
                   .IsRequired();

            builder.Property(x => x.ReferenceId)
                   .IsRequired();

            builder.Property(x => x.BeneficiaryId)
                   .IsRequired();
        }
    }
}