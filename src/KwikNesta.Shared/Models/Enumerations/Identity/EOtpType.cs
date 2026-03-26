using System.ComponentModel;

namespace KwikNesta.Shared.Models.Enumerations.Identity
{
    public enum EOtpType
    {
        [Description("Account Verification")]
        AccountVerification,
        [Description("Password Reset")]
        PasswordReset,
        [Description("Account Reactivation")]
        AccountReactivation
    }
}
