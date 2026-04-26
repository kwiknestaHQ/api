using System.Text.Json.Serialization;

namespace KwikNesta.Shared.ServiceDTOs.Payment
{
    public class PaystackRefundInitiationResponse
    {
        [JsonPropertyName("status")]
        public bool Status { get; set; }
        [JsonPropertyName("message")]
        public string Message { get; set; } = default!;
        [JsonPropertyName("data")]
        public PaystackRefundInitiationResponseData Data { get; set; } = default!;

    }

    public class PaystackRefundInitiationResponseData
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }
        [JsonPropertyName("transaction")]
        public PaystackInitialTransaction Transaction { get; set; } = default!;
        [JsonPropertyName("currency")]
        public string Currency { get; set; } = default!;
        [JsonPropertyName("amount")]
        public decimal Amount { get; set; }
        [JsonPropertyName("status")]
        public string Status { get; set; } = default!;
        [JsonPropertyName("expected_at")]
        public DateTime ExpectedAt { get; set; }
    }

    public class PaystackInitialTransaction
    {
        [JsonPropertyName("reference")]
        public string Reference { get; set; } = default!;
        [JsonPropertyName("amount")]
        public decimal Amount { get; set; }
        [JsonPropertyName("paid_at")]
        public DateTime PaidAt { get; set; }
        [JsonPropertyName("currency")]
        public string Currency { get; set; } = default!;
    }
}