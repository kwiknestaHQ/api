using KwikNesta.Shared.Implementations;
using KwikNesta.Shared.Models.Enumerations.Payments;
using KwikNestaPayment.Domain.Entities;
using KwikNestaPayment.Infrastructure.Contracts;
using KwikNestaPayment.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace KwikNestaPayment.Infrastructure.Repositories
{
    public class FeeRuleRepository(PaymentServiceDbContext context) 
        : Repository<FeeRule>(context), IFeeRuleRepository
    {
        private readonly PaymentServiceDbContext _context = context;

        public async Task<List<FeeRule>> GetFeeRules(EPaymentPurpose appliesTo)
        {
            var now = DateTime.UtcNow;

            return await _context.FeeRules
                .Where(x =>
                    x.IsActive &&
                    x.AppliesTo == appliesTo &&
                    (x.StartsAt == null || x.StartsAt <= now) &&
                    (x.EndsAt == null || x.EndsAt >= now))
                .OrderBy(x => x.Priority)
                .ToListAsync();
        }
    }
}