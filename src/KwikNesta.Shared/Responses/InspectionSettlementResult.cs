using KwikNesta.Shared.Models.Enumerations.Payments;
using KwikNesta.Shared.Models.Settings;

namespace KwikNesta.Shared.Responses
{
    public class InspectionSettlementResult
    {
        public ESettlementOutcome Outcome { get; private set; }
        public decimal LandlordShare { get; private set; }
        public decimal PlatformShare { get; set; }
        public decimal RefundAmount { get; private set; }

        public static InspectionSettlementResult DetermineSettlement(SettlementConfig config, 
                            decimal amount, 
                            bool landlordJoined, 
                            bool tenantJoined)
        {
            return (landlordJoined, tenantJoined) switch
            {
                (true, true) => new InspectionSettlementResult
                {
                    Outcome = ESettlementOutcome.FullSettlement,
                    LandlordShare = amount * config.LandlordFullShare,
                    PlatformShare = amount * config.PlatformFullShare,
                    RefundAmount = 0
                },

                (false, true) => new InspectionSettlementResult
                {
                    Outcome = ESettlementOutcome.LandlordNoShow,
                    LandlordShare = 0,
                    PlatformShare = amount * config.LandlordNoShowFee,
                    RefundAmount = amount * config.TenantNoShowRefund
                },

                (true, false) => new InspectionSettlementResult
                {
                    Outcome = ESettlementOutcome.TenantNoShow,
                    LandlordShare = amount * config.LandlordFullShare,
                    PlatformShare = amount * config.PlatformFullShare,
                    RefundAmount = 0
                },

                (false, false) => new InspectionSettlementResult
                {
                    Outcome = ESettlementOutcome.NoShow,
                    LandlordShare = 0,
                    PlatformShare = 0,
                    RefundAmount = amount
                }
            };
        }
    }
}