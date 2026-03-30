using KwikNesta.Mediator.Cores.Abstractions;
using KwikNesta.Shared.Models;
using KwikNesta.Shared.Responses;
using KwikNesta.Shared.ServiceDTOs.Property;
using Microsoft.AspNetCore.Http;

namespace KwikNesta.Shared.ServiceCommands.Property
{
    public class UploadPropertyMediaCommand : UploadPropertyMediaRequest, IKNRequest<Response<CreatePropertyResponseDto>>
    {
        public Guid PropertyId { get; set; }
        public UserContext UserContext { get; set; } = default!;
    }

    public class UploadPropertyMediaRequest
    {
        public List<IFormFile> Files { get; set; } = [];
    }
}