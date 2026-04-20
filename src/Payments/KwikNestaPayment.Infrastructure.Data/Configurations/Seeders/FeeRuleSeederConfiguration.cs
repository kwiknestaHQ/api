using KwikNesta.Shared.Models.Enumerations.Payments;
using KwikNestaPayment.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KwikNestaPayment.Infrastructure.Data.Configurations.Seeders
{
    public class FeeRuleSeederConfiguration : IEntityTypeConfiguration<FeeRule>
    {
        public void Configure(EntityTypeBuilder<FeeRule> builder)
        {
            var feeRules = new FeeRule[]
            {
                new FeeRule
                {
                    Id = Guid.Parse("86724D52-6968-4B42-BA1D-897D14BD704A"),
                    Name = "Viewing Fee",
                    Type = EFeeType.Charge,
                    CalculationType = EFeeCalculationType.Percentage,
                    Value = 0.007M,
                    MinCap = 2000,
                    MaxCap = 8500,
                    AppliesTo = EPaymentPurpose.Viewing,
                    IsActive = true
                },
                new FeeRule
                {
                    Id = Guid.Parse("86724D52-6968-4B42-BA1D-897D14BD704B"),
                    Name = "Rent Commission",
                    Type = EFeeType.PlatformFee,
                    CalculationType = EFeeCalculationType.Percentage,
                    Value = 0.05M,
                    AppliesTo = EPaymentPurpose.Rent,
                    IsActive = true
                }
            };

            builder.HasData(feeRules);
        }
    }
}