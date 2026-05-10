using KwikNesta.Shared.Models.Enumerations.Infra;
using KwikNestaInfra.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KwikNestaInfra.Infrastructure.Data.Configurations
{
    public class AgoraWebhookEventLogConfiguration : IEntityTypeConfiguration<AgoraWebhookEventLog>
    {
        public void Configure(EntityTypeBuilder<AgoraWebhookEventLog> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .IsRequired();

            builder.Property(x => x.Channel)
               .IsRequired();

            builder.Property(x => x.Type)
                .HasConversion<string>()
               .IsRequired();

            builder.Property(x => x.Module)
               .HasConversion<string>()
              .IsRequired();

            builder.Property(x => x.Platform)
               .HasConversion<string>()
              .HasDefaultValue(EAgoraPlatform.Other);

            builder.Property(x => x.CreatedAt)
                .HasDefaultValueSql("NOW()")
                .IsRequired();

            builder.Property(x => x.Timestamp)
                .IsRequired();

            builder.HasIndex(x => x.Channel);
        }
    }
}