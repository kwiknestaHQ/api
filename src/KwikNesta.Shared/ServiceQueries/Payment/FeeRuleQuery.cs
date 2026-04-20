using KwikNesta.Mediator.Cores.Abstractions;
using KwikNesta.Shared.Models.Enumerations.Payments;
using KwikNesta.Shared.Responses;
using KwikNesta.Shared.ServiceDTOs.Payment;

namespace KwikNesta.Shared.ServiceQueries.Payment
{
    public class FeeRuleQuery : IKNRequest<Response<FeeResult>>
    {
        public EFeeType Type { get; set; }
        public EPaymentPurpose AppliesTo { get; set; }
        public decimal Amount { get; set; }
    }
}