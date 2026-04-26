using System.Text.Json.Serialization;

namespace KwikNesta.Shared.ServiceDTOs.Payment
{
    public class PaystackBanksResponse
    {
        [JsonPropertyName("status")]
        public bool Status { get; set; }
        [JsonPropertyName("message")]
        public string Message { get; set; } = default!;
        [JsonPropertyName("data")]
        public List<PaystackBanksResponseData> Data { get; set; } = [];
    }

    public class PaystackBanksResponseData
    {
        [JsonPropertyName("name")]
        public string Name { get; set; } = default!;
        [JsonPropertyName("code")]
        public string Code { get; set; } = default!;
        [JsonPropertyName("supports_transfer")]
        public bool SupportsTransfer { get; set; }
        [JsonPropertyName("type")]
        public string Type { get; set; } = default!;
        [JsonPropertyName("currency")]
        public string Currency { get; set; } = default!;
        [JsonPropertyName("active")]
        public bool IsActive { get; set; }
        [JsonPropertyName("is_deleted")]
        public bool IsDeleted { get; set; }
    }
}