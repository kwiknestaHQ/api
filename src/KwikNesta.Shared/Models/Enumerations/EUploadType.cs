using System.ComponentModel;

namespace KwikNesta.Shared.Models.Enumerations
{
    public enum EUploadType
    {
        [Description("images")]
        Image,
        [Description("docs")]
        Docs,
        [Description("videos")]
        Video,
        [Description("audios")]
        Audio
    }
}