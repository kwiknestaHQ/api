using KwikNestaProperty.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KwikNestaProperty.Infrastructure.Data.Configurations
{
    internal class ViewingRequestConfiguration : IEntityTypeConfiguration<ViewingRequest>
    {
        public void Configure(EntityTypeBuilder<ViewingRequest> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Status)
                .HasConversion<string>()
                .IsRequired();

            builder.Property(x => x.UserId)
                .IsRequired();

            builder.Property(x => x.RequestedDate)
                .IsRequired();

            builder.HasOne(x => x.Property)
               .WithMany(x => x.ViewingRequests)
               .HasForeignKey(x => x.PropertyId)
               .OnDelete(DeleteBehavior.Restrict);
        }
    }
}