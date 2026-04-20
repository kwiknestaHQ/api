using KwikNesta.Shared.Implementations;
using KwikNestaPayment.Domain.Entities;
using KwikNestaPayment.Infrastructure.Contracts;
using KwikNestaPayment.Infrastructure.Data;

namespace KwikNestaPayment.Infrastructure.Repositories
{
    public class PaymentRepository(PaymentServiceDbContext context) 
        : Repository<KNPayment>(context), IPaymentRepository
    {
    }
}