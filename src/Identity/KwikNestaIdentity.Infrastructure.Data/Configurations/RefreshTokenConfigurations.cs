using KwikNestaIdentity.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KwikNestaIdentity.Infrastructure.Data.Configurations
{
    internal class RefreshTokenConfigurations : IEntityTypeConfiguration<RefreshToken>
    {
        public void Configure(EntityTypeBuilder<RefreshToken> builder)
        {
            builder.HasKey(u => u.Id);

            builder.HasOne(x => x.User)
                  .WithMany()
                  .HasForeignKey(x => x.UserId);

            builder.Property(u => u.CreatedOn)
                   .IsRequired();

            builder.Property(u => u.TokenHash)
                .IsRequired();

            builder.Property(u => u.ExpiresAt)
                .IsRequired();

            builder.HasIndex(x => new { x.TokenHash })
                .IsUnique();
        }
    }
}