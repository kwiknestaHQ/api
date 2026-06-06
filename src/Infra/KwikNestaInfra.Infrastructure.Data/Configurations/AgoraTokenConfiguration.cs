using KwikNestaInfra.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KwikNestaInfra.Infrastructure.Data.Configurations
{
    public class AgoraTokenConfiguration : IEntityTypeConfiguration<AgoraToken>
    {
        public void Configure(EntityTypeBuilder<AgoraToken> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .IsRequired();

            builder.Property(x => x.ChannelName)
               .IsRequired();

            builder.Property(x => x.Account)
              .IsRequired();

            builder.Property(x => x.Token)
               .IsRequired();

            builder.Property(x => x.ExpiresAt)
                .IsRequired();

            builder.HasIndex(x => x.ChannelName);
        }
    }
}