using System.ComponentModel;

namespace KwikNesta.Shared.Models.Enumerations.Property
{
    public enum ESessionParticipantRole
    {
        [Description("Host")]
        Publisher,
        [Description("Audience")]
        Subscriber
    }
}