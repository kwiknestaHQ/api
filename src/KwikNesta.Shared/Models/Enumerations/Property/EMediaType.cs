using System.ComponentModel;

namespace KwikNesta.Shared.Models.Enumerations.Property
{
    public enum EMediaType : byte
    {
        [Description("Image")]
        Image,
        [Description("Video")]
        Video,
        [Description("Document")]
        Document
    }
}
