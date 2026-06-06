namespace KwikNesta.Shared.Models.Settings
{
    public class SettlementConfig
    {
        public decimal LandlordFullShare { get; set; } = 0.60m;
        public decimal PlatformFullShare { get; set; } = 0.40m;
        public decimal LandlordNoShowFee { get; set; } = 0.10m;
        public decimal TenantNoShowRefund { get; set; } = 0.90m;
    }
}