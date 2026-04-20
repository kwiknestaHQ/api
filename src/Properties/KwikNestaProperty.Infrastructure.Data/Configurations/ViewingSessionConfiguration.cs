using KwikNestaProperty.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KwikNestaProperty.Infrastructure.Data.Configurations
{
    public class ViewingSessionConfiguration : IEntityTypeConfiguration<ViewingSession>
    {
        public void Configure(EntityTypeBuilder<ViewingSession> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.ChannelName)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(x => x.Status)
                .HasConversion<string>()
                .IsRequired();

            builder.Property(x => x.ScheduledStart)
                .IsRequired();

            builder.HasOne(x => x.ViewingRequest)
                .WithOne(p => p.Session)
                .HasForeignKey<ViewingSession>(x => x.ViewingRequestId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}