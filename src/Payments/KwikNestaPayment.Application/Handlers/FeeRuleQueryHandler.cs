using KwikNesta.Mediator.Cores.Abstractions;
using KwikNesta.Shared.Models.Enumerations.Payments;
using KwikNesta.Shared.Responses;
using KwikNesta.Shared.ServiceDTOs.Payment;
using KwikNesta.Shared.ServiceQueries.Payment;
using KwikNestaPayment.Infrastructure;

namespace KwikNestaPayment.Application.Handlers
{
    public class FeeRuleQueryHandler(IPaymentRepositoryManager repository) : IKNRequestHandler<FeeRuleQuery, Response<FeeResult>>
    {
        private readonly IPaymentRepositoryManager _repository = repository;

        public async Task<Response<FeeResult>> HandleAsync(FeeRuleQuery request, CancellationToken cancellationToken)
        {
            var rules = await _repository.FeeRule
                .GetFeeRules(request.AppliesTo);

            decimal charge = 0;
            decimal platformFee = 0;
            decimal discount = 0;

            foreach (var rule in rules)
            {
                var amount = rule.CalculationType switch
                {
                    EFeeCalculationType.Flat => rule.Value,
                    EFeeCalculationType.Percentage => request.Amount * rule.Value,
                    _ => 0
                };

                if (rule.MinCap.HasValue)
                {
                    amount = Math.Max(amount, rule.MinCap.Value);
                }

                if (rule.MaxCap.HasValue)
                {
                    amount = Math.Min(amount, rule.MaxCap.Value);
                }

                switch (rule.Type)
                {
                    case EFeeType.Charge:
                        charge += amount;
                        break;

                    case EFeeType.PlatformFee:
                        platformFee += amount;
                        break;

                    case EFeeType.Discount:
                        discount += amount;
                        break;
                }
            }

            var finalAmount = charge - discount;

            return Response<FeeResult>.Ok(new FeeResult
            {
                Charge = charge,
                PlatformFee = Math.Max(platformFee, 0),
                Discount = discount,
                FinalAmount = finalAmount
            });
        }
    }
}