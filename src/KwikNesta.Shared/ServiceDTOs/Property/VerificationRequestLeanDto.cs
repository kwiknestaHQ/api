using KwikNesta.Shared.Extensions;
using KwikNesta.Shared.Models.Enumerations.Property;
using System.Text.Json.Serialization;

namespace KwikNesta.Shared.ServiceDTOs.Property
{
    public class VerificationRequestLeanDto
    {
        public Guid Id { get; set; }
        public Guid PropertyId { get; set; }
        public DateTime SubmittedOn { get; set; }
        public EVerificationStatus Status { get; set; }
        public string? Reason { get; set; }
        public DateTime? ReviewedOn { get; set; }
        public string StatusText => Status.GetDescription();
    }

    public class VerificationRequestDto
    {
        public Guid Id { get; set; }
        [JsonIgnore]
        public Guid PropertyId { get; set; }
        public DateTime SubmittedOn { get; set; }
        public EVerificationStatus Status { get; set; }
        public string? Reason { get; set; }
        public DateTime? ReviewedOn { get; set; }
        public string StatusText => Status.GetDescription();
        public PropertyLeanDto Property { get; set; } = default!;
        public PropertyOwnerLean Owner { get; set; } = default!;
        public List<PropertyDocumentDto> Documents { get; set; } = [];
        public List<VerificationRequestLeanDto> PreviousRequests { get; set; } = [];
    }
}