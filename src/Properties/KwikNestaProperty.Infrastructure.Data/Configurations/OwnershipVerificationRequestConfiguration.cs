using KwikNestaProperty.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KwikNestaProperty.Infrastructure.Data.Configurations
{
    internal class OwnershipVerificationRequestConfiguration : IEntityTypeConfiguration<OwnershipVerificationRequest>
    {
        public void Configure(EntityTypeBuilder<OwnershipVerificationRequest> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Status)
                .HasConversion<string>()
                .IsRequired();

            builder.Property(x => x.OwnerId)
                .IsRequired();

            builder.Property(x => x.AdminComment)
                .HasMaxLength(500);

            builder.HasOne(x => x.Property)
               .WithMany(x => x.OwnershipVerificationRequests)
               .HasForeignKey(x => x.PropertyId)
               .OnDelete(DeleteBehavior.Restrict);
        }
    }
}