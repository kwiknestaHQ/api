using KwikNestaProperty.Domain.Entities;
using KwikNestaProperty.Infrastructure.Data.Configurations;
using KwikNestaProperty.Infrastructure.Data.Configurations.DbSeeder;
using Microsoft.EntityFrameworkCore;

namespace KwikNestaProperty.Infrastructure.Data
{
    public class PropertyServiceDbContext(DbContextOptions<PropertyServiceDbContext> options) 
        : DbContext(options)
    {
        public DbSet<KNProperty> Properties { get; set; }
        public DbSet<PropertyLocation> Locations { get; set; }
        public DbSet<ViewingRequest> ViewingRequests { get; set; }
        public DbSet<PropertyMedia> PropertyMedias { get; set; }
        public DbSet<PropertyFeature> PropertyFeatures { get; set; }
        public DbSet<OwnershipVerificationRequest> OwnershipVerificationRequests { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.HasDefaultSchema("kn-property-svc");
            builder.ApplyConfiguration(new PropertyLocationConfiguration());
            builder.ApplyConfiguration(new PropertyFeatureConfiguration());
            builder.ApplyConfiguration(new PropertyMediaConfiguration());
            builder.ApplyConfiguration(new ViewingRequestConfiguration());
            builder.ApplyConfiguration(new OwnershipVerificationRequestConfiguration());
            builder.ApplyConfiguration(new KNPropertyConfiguration());
            builder.ApplyConfiguration(new PropertyFeatureLinkConfiguration());
            builder.ApplyConfiguration(new PropertyFeatureSeederConfiguration());

            base.OnModelCreating(builder);
        }
    }
}