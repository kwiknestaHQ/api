using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace KwikNesta.Shared.ServiceDTOs.Payment
{
    public class PaystackInitResult
    {
        [JsonPropertyName("status")]
        public bool Status { get; set; }
        [JsonPropertyName("message")]
        public string Message { get; set; } = default!;
        [JsonPropertyName("data")]
        public PaystackChargeData Data { get; set; } = default!;
    }

    public class PaystackChargeData
    {
        [JsonPropertyName("authorization_url")]
        public string AuthorizationUrl { get; set; } = default!;
        [JsonPropertyName("access_code")]
        public string AccessCode { get; set; } = default!;
        [JsonPropertyName("reference")]
        public string Reference { get; set; } = default!;
    }
}