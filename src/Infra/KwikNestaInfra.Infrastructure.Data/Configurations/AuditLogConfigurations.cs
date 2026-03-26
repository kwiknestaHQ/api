using KwikNestaInfra.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KwikNestaIdentity.Infrastructure.Data.Configurations
{
    internal class AuditLogConfigurations : IEntityTypeConfiguration<AuditLog>
    {
        public void Configure(EntityTypeBuilder<AuditLog> builder)
        {
            builder.Property(a => a.UserId)
                .IsRequired();

            builder.Property(x => x.UserName)
                 .IsRequired();

            builder.Property(a => a.DomainId)
               .IsRequired();
            builder.HasIndex(a => a.DomainId);

            builder.Property(a => a.Action)
                .HasConversion<string>()
                .IsRequired();

            builder.Property(a => a.Domain)
                .HasConversion<string>()
                .IsRequired();

            builder.Property(a => a.CreatedOn)
                .IsRequired();
        }
    }
}