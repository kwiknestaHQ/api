using System.Text.Json.Serialization;

namespace KwikNesta.Shared.ServiceDTOs.Payment
{
    public class PaystackRefundQueryResponse
    {
        [JsonPropertyName("status")]
        public bool Status { get; set; }
        [JsonPropertyName("message")]
        public string Message { get; set; } = default!;
        [JsonPropertyName("data")]
        public PaystackRefundQueryResponseData Data { get; set; } = default!;
    }

    public class PaystackRefundQueryResponseData
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }
        [JsonPropertyName("transaction")]
        public long Transaction { get; set; }
        [JsonPropertyName("refunded_at")]
        public DateTime RefundedAt { get; set; }
        [JsonPropertyName("amount")]
        public decimal Amount { get; set; }
        [JsonPropertyName("deducted_amount")]
        public decimal AmountDeducted { get; set; }
        [JsonPropertyName("transaction_amount")]
        public decimal TransactionAmount { get; set; }
        [JsonPropertyName("createdAt")]
        public DateTime CreatedAt { get; set; }
        [JsonPropertyName("status")]
        public string Status { get; set; } = default!;
        [JsonPropertyName("reason")]
        public string? Reason { get; set; }
        [JsonPropertyName("transaction_reference")]
        public string TransactionReference { get; set; } = default!;

    }
}