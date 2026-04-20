using KwikNestaPayment.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace KwikNestaPayment.Infrastructure.Data
{
    public class PaymentServiceDbContext(DbContextOptions<PaymentServiceDbContext> options) 
        : DbContext(options)
    {
        public DbSet<KNPayment> Payments { get; set; }
        public DbSet<KNSettlement> Settlements { get; set; }
        public DbSet<FeeRule> FeeRules { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.HasDefaultSchema("kn-payment-svc");
            builder.ApplyConfigurationsFromAssembly(typeof(PaymentServiceDbContext).Assembly);

            base.OnModelCreating(builder);
        }
    }
}