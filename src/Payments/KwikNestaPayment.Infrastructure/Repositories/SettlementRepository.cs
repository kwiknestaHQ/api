using KwikNesta.Shared.Implementations;
using KwikNestaPayment.Domain.Entities;
using KwikNestaPayment.Infrastructure.Contracts;
using KwikNestaPayment.Infrastructure.Data;

namespace KwikNestaPayment.Infrastructure.Repositories
{
    public class SettlementRepository(PaymentServiceDbContext context) 
        : Repository<KNSettlement>(context), ISettlementRepository
    {
    }
}