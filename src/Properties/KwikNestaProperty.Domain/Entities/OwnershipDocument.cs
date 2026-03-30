using KwikNesta.Shared.Models;
using KwikNesta.Shared.Models.Enumerations.Property;

namespace KwikNestaProperty.Domain.Entities
{
    public class OwnershipDocument : BaseEntity
    {
        public Guid VerificationRequestId { get; set; }
        public OwnershipVerificationRequest VerificationRequest { get; set; } = default!;

        public string FileUrl { get; set; } = default!;
        public EDocumentType DocumentType { get; set; }
        public string? OtherDocumentType { get; set; }
    }
}