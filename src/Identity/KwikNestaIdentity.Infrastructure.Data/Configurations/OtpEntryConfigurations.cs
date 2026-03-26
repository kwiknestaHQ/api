using KwikNestaIdentity.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KwikNestaIdentity.Infrastructure.Data.Configurations
{
    internal class OtpEntryConfigurations : IEntityTypeConfiguration<OtpEntry>
    {
        public void Configure(EntityTypeBuilder<OtpEntry> builder)
        {
            builder.HasKey(u => u.Id);

            builder.HasOne(x => x.User)
                  .WithMany()
                  .HasForeignKey(x => x.UserId);

            builder.Property(u => u.CreatedOn)
                   .IsRequired();

            builder.Property(u => u.OtpHash)
                .IsRequired();

            builder.Property(u => u.ExpiresAt)
                .IsRequired();

            builder.Property(u => u.Type)
                   .HasConversion<string>()
                   .IsRequired();

            builder.HasIndex(x => new { x.UserId, x.Type });
        }
    }
}