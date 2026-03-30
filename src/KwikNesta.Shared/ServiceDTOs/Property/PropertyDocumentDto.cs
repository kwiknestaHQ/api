using KwikNesta.Shared.Models.Enumerations.Property;

namespace KwikNesta.Shared.ServiceDTOs.Property
{
    public class PropertyDocumentDto
    {
        public Guid Id { get; set; }
        public string FileUrl { get; set; } = default!;
        public EDocumentType DocumentType { get; set; }
        public string DocumentTypeText { get; set; } = default!;
        public DateTime UploadedAt { get; set; }
    }
}