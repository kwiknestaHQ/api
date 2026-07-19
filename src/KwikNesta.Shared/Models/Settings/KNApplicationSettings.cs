namespace KwikNesta.Shared.Models.Settings
{
    public class KNApplicationSettings
    {
        public JwtSettings Jwt { get; set; } = default!;
        public ResendSettings Resend { get; set; } = default!;
        public KNAdminSettings AppAdmin {  get; set; } = default!;
        public CsApiSettings CsApi { get; set; } = default!;
        public KNUploadSettings Upload { get; set; } = default!;
        public PaystackSettings Paystack { get; set; } = default!;
        public AgoraSetting Agora { get; set; } = default!;
        public SettlementConfig Settlement { get; set; } = default!;
        public ViewingCheckInSettings CheckIn { get; set; } = default!;
    }
}