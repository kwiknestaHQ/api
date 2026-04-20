using KwikNestaPayment.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KwikNestaPayment.Infrastructure.Data.Configurations
{
    public class FeeRuleConfiguration : IEntityTypeConfiguration<FeeRule>
    {
        public void Configure(EntityTypeBuilder<FeeRule> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                  .IsRequired()
                  .HasMaxLength(50);

            builder.Property(x => x.Type)
                   .IsRequired()
                   .HasConversion<string>();

            builder.Property(x => x.CalculationType)
                   .IsRequired()
                   .HasConversion<string>();

            builder.Property(x => x.Value)
                   .HasPrecision(18, 3)
                   .IsRequired();

            builder.Property(x => x.MinCap)
                   .HasPrecision(18, 2)
                   .IsRequired(false);

            builder.Property(x => x.MaxCap)
                   .HasPrecision(18, 2)
                   .IsRequired(false);

            builder.Property(x => x.AppliesTo)
                   .IsRequired()
                   .HasConversion<string>();

            builder.HasIndex(x => new { x.IsActive, x.AppliesTo, x.StartsAt, x.EndsAt, x.Priority });
        }
    }
}