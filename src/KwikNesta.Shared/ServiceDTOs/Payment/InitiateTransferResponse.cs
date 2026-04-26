using System.Text.Json.Serialization;

namespace KwikNesta.Shared.ServiceDTOs.Payment
{
    public class InitiateTransferResponse
    {
        [JsonPropertyName("status")]
        public bool Status { get; set; }
        [JsonPropertyName("message")]
        public string Message { get; set; } = default!;
        [JsonPropertyName("data")]
        public InitiateTransferResponseData Data { get; set; } = default!;
    }

    public class InitiateTransferResponseData
    {

    }
}