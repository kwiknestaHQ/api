using KwikNestaInfra.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace KwikNestaInfra.Infrastructure.Data
{
    public class InfraServiceDbContext(DbContextOptions<InfraServiceDbContext> options) 
        : DbContext(options)
    {
        public DbSet<AuditLog> AuditLogs { get; set; }
        public DbSet<KNCountry> Countries { get; set; }
        public DbSet<KNState> States { get; set; }
        public DbSet<KNCity> Cities { get; set; }
        public DbSet<KNTimeZone> TimeZones { get; set; }
        public DbSet<AgoraWebhookEventLog> AgoraWebhookEvents { get; set; }
        public DbSet<AgoraToken> AgoraTokens { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.HasDefaultSchema("kn-infra-svc");
            builder.ApplyConfigurationsFromAssembly(typeof(InfraServiceDbContext).Assembly);

            base.OnModelCreating(builder);
        }
    }
}