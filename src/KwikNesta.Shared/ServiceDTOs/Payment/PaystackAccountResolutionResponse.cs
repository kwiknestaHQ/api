using System.Text.Json.Serialization;

namespace KwikNesta.Shared.ServiceDTOs.Payment
{
    public class PaystackAccountResolutionResponse
    {
        [JsonPropertyName("status")]
        public bool Status { get; set; }
        [JsonPropertyName("message")]
        public string Message { get; set; } = default!;
        [JsonPropertyName("data")]
        public PaystackAccountResolutionResponseData Data { get; set; } = default!;
    }

    public class PaystackAccountResolutionResponseData
    {
        [JsonPropertyName("account_number")]
        public string AccountNumber { get; set; } = default!;
        [JsonPropertyName("account_name")]
        public string AccountName { get; set; } = default!;
    }
}