using KwikNestaIdentity.Domain.Entities;
using KwikNestaIdentity.Infrastructure.Data.Configurations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace KwikNestaIdentity.Infrastructure.Data
{
    public class IdentityServiceDbContext : IdentityDbContext<User>
    {
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<OtpEntry> OtpEntries { get; set; }

        public IdentityServiceDbContext(DbContextOptions<IdentityServiceDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.HasDefaultSchema("kn-identity-svc");
            builder.ApplyConfiguration(new UserConfigurations());
            builder.ApplyConfiguration(new OtpEntryConfigurations());
            builder.ApplyConfiguration(new RefreshTokenConfigurations());
            builder.ApplyConfiguration(new RoleConfigurations());
            
            base.OnModelCreating(builder);
        }
    }
}