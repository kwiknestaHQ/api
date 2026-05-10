using KwikNesta.Shared.Models.Enumerations.Infra;
using System.Text.Json.Serialization;

namespace KwikNesta.Shared.ServiceDTOs.Infra
{
    public class AgoraWebhookPayload<T> where T : AgoraPayloadBase
    {
        [JsonPropertyName("noticeId")]
        public string NoticeId { get; set; } = default!;
        [JsonPropertyName("eventType")]
        public int EventType { get; set; }
        public EAgoraEvent Event => (EAgoraEvent)EventType;
        [JsonPropertyName("payload")]
        public T Payload { get; set; } = default!;
    }

    public class Agora101Payload : AgoraPayloadBase { }

    public class Agora102Payload : AgoraPayloadBase
    {
        [JsonPropertyName("lastUid")]
        public uint LastUid { get; set; }
    }

    public class Agora103Payload : AgoraJoinPayloadBase { }

    public class Agora104Payload : AgoraLeavePayloadBase { }

    public class Agora105Payload : AgoraJoinPayloadBase { }

    public class Agora106Payload : AgoraLeavePayloadBase { }

    public abstract class AgoraPayloadBase
    {
        [JsonPropertyName("channelName")]
        public string ChanelName { get; set; } = default!;
        [JsonPropertyName("ts")]
        public long Timestamp { get; set; }
    }

    public abstract class AgoraJoinPayloadBase : AgoraPayloadBase
    {
        [JsonPropertyName("uid")]
        public uint Uid { get; set; }
        [JsonPropertyName("platform")]
        public int Platform { get; set; }
        public EAgoraPlatform EPlatform => (EAgoraPlatform)Platform;
    }

    public abstract class AgoraLeavePayloadBase : AgoraPayloadBase
    {
        [JsonPropertyName("uid")]
        public uint Uid { get; set; }
        [JsonPropertyName("platform")]
        public int Platform { get; set; }
        [JsonPropertyName("duration")]
        public int Duration { get; set; }
        [JsonPropertyName("reason")]
        public int Reason { get; set; }
    }
}