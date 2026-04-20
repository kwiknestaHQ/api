using KwikNestaProperty.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KwikNestaProperty.Infrastructure.Data.Configurations
{
    public class PropertyInquiryConfiguration : IEntityTypeConfiguration<PropertyInquiry>
    {
        public void Configure(EntityTypeBuilder<PropertyInquiry> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.UserId)
                .IsRequired();

            builder.Property(x => x.OfferedPrice)
                .HasPrecision(18, 2)
                .IsRequired();

            builder.HasOne(x => x.Property)
               .WithMany(x => x.Inquiries)
               .HasForeignKey(x => x.PropertyId)
               .OnDelete(DeleteBehavior.Restrict);
        }
    }
}