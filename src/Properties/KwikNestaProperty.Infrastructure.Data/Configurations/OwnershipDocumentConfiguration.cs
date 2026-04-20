using KwikNestaProperty.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KwikNestaProperty.Infrastructure.Data.Configurations
{
    public class OwnershipDocumentConfiguration : IEntityTypeConfiguration<OwnershipDocument>
    {
        public void Configure(EntityTypeBuilder<OwnershipDocument> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.DocumentType)
                .HasConversion<string>()
                .IsRequired();

            builder.Property(x => x.OtherDocumentType)
                .HasMaxLength(50);

            builder.Property(x => x.FileUrl)
                .IsRequired()
                .HasMaxLength(250);

            builder.HasOne(x => x.VerificationRequest)
               .WithMany(x => x.Documents)
               .HasForeignKey(x => x.VerificationRequestId)
               .OnDelete(DeleteBehavior.Cascade);
        }
    }
}