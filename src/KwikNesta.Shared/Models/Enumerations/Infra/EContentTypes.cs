using System.ComponentModel;

namespace KwikNesta.Shared.Models.Enumerations.Infra
{
    public enum EContentTypes
    {
        [Description("image/jpeg")]
        Jpeg,
        [Description("image/png")]
        Png,
        [Description("image/gif")]
        Gif,
        [Description("application/pdf")]
        Pdf,
        [Description("audio/mpeg")]
        Mpeg,
        [Description("video/mp4")]
        Mp4,
        [Description("text/csv")]
        Csv,
        [Description("application/vnd.ms-excel")]
        Xls,
        [Description("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")]
        Xlsx,
        [Description("application/octet-stream")]
        NotSpecified
    }
}