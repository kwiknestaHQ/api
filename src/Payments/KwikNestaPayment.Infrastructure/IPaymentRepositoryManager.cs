using KwikNesta.Shared.Contracts;
using KwikNestaPayment.Infrastructure.Contracts;

namespace KwikNestaPayment.Infrastructure
{
    public interface IPaymentRepositoryManager : IBaseRepositoryManager
    {
        IPaymentRepository Payment {  get; }
        ISettlementRepository Settlement { get; }
        IFeeRuleRepository FeeRule { get; }
        IPayoutAccountRepository PayoutAccount { get; }
        IRefundRepository Refund { get; }
        ITransferRepository Transfer { get; }
    }
}