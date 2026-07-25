using KwikNestaProperty.Domain.Entities;
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
        public DbSet<PropertyFeatureLink> PropertyFeatureLinks { get; set; }
        public DbSet<OwnershipVerificationRequest> OwnershipVerificationRequests { get; set; }
        public DbSet<OwnershipDocument> OwnershipDocuments { get; set; }
        public DbSet<PropertyInquiry> PropertyInquiries { get; set; }
        public DbSet<PropertyPriceHistory> PropertyPriceHistories { get; set; }
        public DbSet<PropertyView> PropertyViews { get; set; }
        public DbSet<ViewingSession> ViewingSessions { get; set; }
        public DbSet<SessionParticipant> SessionParticipants { get; set; }
        public DbSet<ViewingCheckIn> ViewingCheckIns { get; set; }
        public DbSet<RentalIntent> RentalIntents { get; set; }
        public DbSet<RentalPayment> RentalPayments { get; set; }
        public DbSet<TenancyAgreement> TenancyAgreements { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.HasDefaultSchema("kn-property-svc");
            builder.ApplyConfigurationsFromAssembly(typeof(PropertyServiceDbContext).Assembly);
            base.OnModelCreating(builder);
        }
    }
}