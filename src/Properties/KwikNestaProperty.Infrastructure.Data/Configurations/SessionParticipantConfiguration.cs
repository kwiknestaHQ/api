using KwikNestaProperty.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KwikNestaProperty.Infrastructure.Data.Configurations
{
    public class SessionParticipantConfiguration : IEntityTypeConfiguration<SessionParticipant>
    {
        public void Configure(EntityTypeBuilder<SessionParticipant> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.UserId)
                .IsRequired();

            builder.Property(x => x.Role)
                .HasConversion<string>()
                .IsRequired();

            builder.HasIndex(x => new { x.ViewingSessionId, x.UId });

            builder.HasOne(x => x.ViewingSession)
               .WithMany(x => x.Participants)
               .HasForeignKey(x => x.ViewingSessionId)
               .OnDelete(DeleteBehavior.Cascade);
        }
    }
}