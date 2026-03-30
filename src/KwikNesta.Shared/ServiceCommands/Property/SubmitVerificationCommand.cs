using KwikNesta.Mediator.Cores.Abstractions;
using KwikNesta.Shared.Models;
using KwikNesta.Shared.Models.Enumerations.Property;
using KwikNesta.Shared.Responses;
using Microsoft.AspNetCore.Http;

namespace KwikNesta.Shared.ServiceCommands.Property
{
    public class SubmitVerificationCommand : SubmitVerificationRequest, IKNRequest<Response<string>>
    {
        public Guid PropertyId { get; set; }
        public UserContext UserContext { get; set; } = default!;
    }

    public class SubmitVerificationRequest
    {
        public List<VerificationDocumentUploadDto> Documents { get; set; } = new();
    }

    public class VerificationDocumentUploadDto
    {
        public EDocumentType DocumentType { get; set; }
        public string? OtherDocumentType { get; set; }
        public IFormFile File { get; set; } = default!;
    }
}