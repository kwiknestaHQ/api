using KwikNestaIdentity.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KwikNestaIdentity.Infrastructure.Data.Configurations
{
    internal class UserConfigurations : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);

            builder.Property(u => u.FirstName)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(u => u.LastName)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(u => u.OtherName)
                .HasMaxLength(100);

            builder.Property(u => u.PhoneNumber)
                   .IsRequired()
                   .HasMaxLength(15);

            builder.HasIndex(u => u.PhoneNumber)
                   .IsUnique();

            builder.Property(u => u.CreatedOn)
                   .IsRequired();

            builder.Property(u => u.Email)
                   .HasMaxLength(200);

            builder.Property(u => u.Gender)
                   .HasConversion<string>()
                   .IsRequired();

            builder.Property(u => u.Status)
                   .HasConversion<string>()
                   .IsRequired();
        }
    }
}