using KwikNestaProperty.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KwikNestaProperty.Infrastructure.Data.Configurations
{
    public class ViewingCheckInConfiguration : IEntityTypeConfiguration<ViewingCheckIn>
    {
        public void Configure(EntityTypeBuilder<ViewingCheckIn> builder)
        {
            builder.HasKey(c => c.Id);
            builder.HasOne(c => c.ViewingRequest)
             .WithMany()
             .HasForeignKey(c => c.ViewingRequestId)
             .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(c => new { c.ViewingRequestId, c.UserId }).IsUnique();
        }
    }
}