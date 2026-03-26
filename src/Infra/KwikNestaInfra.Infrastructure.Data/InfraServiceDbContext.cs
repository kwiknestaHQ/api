using KwikNestaIdentity.Infrastructure.Data.Configurations;
using KwikNestaInfra.Domain.Entities;
using KwikNestaInfra.Infrastructure.Data.Configurations;
using Microsoft.EntityFrameworkCore;

namespace KwikNestaInfra.Infrastructure.Data
{
    public class InfraServiceDbContext : DbContext
    {
        public DbSet<AuditLog> AuditLogs { get; set; }
        public DbSet<KNCountry> Countries { get; set; }
        public DbSet<KNState> States { get; set; }
        public DbSet<KNCity> Cities { get; set; }
        public DbSet<KNTimeZone> TimeZones { get; set; }

        public InfraServiceDbContext(DbContextOptions<InfraServiceDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.HasDefaultSchema("kn-infra-svc");
            builder.ApplyConfiguration(new AuditLogConfigurations());
            builder.ApplyConfiguration(new KNCountryConfiguration());
            builder.ApplyConfiguration(new KNStateConfiguration());
            builder.ApplyConfiguration(new KNCityConfiguration());
            builder.ApplyConfiguration(new KNTimeZoneConfiguration());

            base.OnModelCreating(builder);
        }
    }
}