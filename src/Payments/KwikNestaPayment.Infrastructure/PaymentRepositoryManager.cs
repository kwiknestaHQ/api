using KwikNestaPayment.Infrastructure.Contracts;
using KwikNestaPayment.Infrastructure.Data;
using KwikNestaPayment.Infrastructure.Repositories;

namespace KwikNestaPayment.Infrastructure
{
    public class PaymentRepositoryManager(PaymentServiceDbContext context) 
        : IPaymentRepositoryManager
    {
        private readonly PaymentServiceDbContext _context = context;
        private readonly Lazy<IPaymentRepository> _payment = new(()
            => new PaymentRepository(context));
        private readonly Lazy<ISettlementRepository> _settlement = new(()
            => new SettlementRepository(context));
        private readonly Lazy<IFeeRuleRepository> _feeRule = new(()
            => new FeeRuleRepository(context));
        private readonly Lazy<IPayoutAccountRepository> _payoutAccount = new(()
            => new PayoutAccountRepository(context));
        private readonly Lazy<ITransferRepository> _transfer = new(()
            => new TransferRepository(context));
        private readonly Lazy<IRefundRepository> _refund = new(()
            => new RefundRepository(context));

        public IPaymentRepository Payment => _payment.Value;
        public ISettlementRepository Settlement => _settlement.Value;
        public IFeeRuleRepository FeeRule => _feeRule.Value;
        public IPayoutAccountRepository PayoutAccount => _payoutAccount.Value;
        public IRefundRepository Refund => _refund.Value;
        public ITransferRepository Transfer => _transfer.Value;

        public async Task BeginTransaction(Func<Task> action)
        {
            if (_context.Database.CurrentTransaction != null)
            {
                await action();
                return;
            }

            await using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                await action();

                await SaveAsync();
                await transaction.CommitAsync();

            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}