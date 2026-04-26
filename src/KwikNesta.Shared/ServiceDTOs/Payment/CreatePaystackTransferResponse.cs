using System.Text.Json.Serialization;

namespace KwikNesta.Shared.ServiceDTOs.Payment
{
    public class CreatePaystackTransferResponse
    {
        [JsonPropertyName("status")]
        public bool Status { get; set; }
        [JsonPropertyName("message")]
        public string Message { get; set; } = default!;
        [JsonPropertyName("data")]
        public CreatePaystackTransferResponseData Data { get; set; } = default!;
    }

    public class CreatePaystackTransferResponseData
    {
        [JsonPropertyName("active")]
        public bool Active { get; set; }
        [JsonPropertyName("currency")]
        public string Currency { get; set; } = default!;
        [JsonPropertyName("type")]
        public string Type { get; set; } = default!;
        [JsonPropertyName("recipient_code")]
        public string RecipientCode { get; set; } = default!;
        [JsonPropertyName("details")]
        public CreatePaystackTransferResponseDataDetails Details { get; set; } = default!;
    }

    public class CreatePaystackTransferResponseDataDetails
    {
        [JsonPropertyName("account_number")]
        public string AccountNumber { get; set; } = default!;
        [JsonPropertyName("account_name")]
        public string AccountName { get; set; } = default!;
        [JsonPropertyName("bank_code")]
        public string BankCode { get; set; } = default!;
        [JsonPropertyName("bank_name")]
        public string BankName { get; set; } = default!;
    }
}