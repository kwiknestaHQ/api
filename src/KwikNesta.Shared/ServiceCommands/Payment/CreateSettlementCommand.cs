using KwikNesta.Mediator.Cores.Abstractions;
using KwikNesta.Shared.Models.Enumerations.Payments;
using KwikNesta.Shared.Responses;

namespace KwikNesta.Shared.ServiceCommands.Payment
{
    public class CreateSettlementCommand : IKNRequest<Response<string>>
    {
        public string UserId { get; set; } = default!;
        public decimal Amount { get; set; }
        public EPaymentPurpose Purpose { get; set; }
        public EPayoutType SettlementType { get; set; }
        public Guid ReferenceId { get; set; }
    }
}