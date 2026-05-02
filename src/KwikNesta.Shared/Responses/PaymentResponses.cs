
namespace KwikNesta.Shared.Responses
{
    public static class PaymentResponses
    {
        public static readonly string RecordNotFound = "{0} record not found";
        public static readonly string InvalidRequest = "Invalid request";
        public static readonly string PaymentInitFailed = "Payment initialization failed.";
        public static readonly string RefundFailed = "Refund failed for {0}";
        public static readonly string RefundQueryFailed = "Refund query failed for Id: {0}";
        public static readonly string TransferCreationFailed = "Transfer creation failed for {0}";
        public static readonly string ExistingUserAccount = "User already has an existing account.";
        public static readonly string AccountNumberAdded = "Account number successfully added.";
    }
}