using System.ComponentModel;

namespace KwikNesta.Shared.Models.Enumerations.Infra
{
    public enum EAgoraEvent
    {
        [Description("Session Started")]
        ChannelCreated = 101,
        [Description("Session Ended")]
        ChannelDestroyed,
        [Description("Host Joins")]
        HostJoins,
        [Description("Host Leaves")]
        HostLeaves,
        [Description("Audience Joins")]
        AudienceJoins,
        [Description("Audience Leaves")]
        AudienceLeaves
    }
}