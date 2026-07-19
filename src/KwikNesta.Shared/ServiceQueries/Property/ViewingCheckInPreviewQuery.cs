using KwikNesta.Mediator.Cores.Abstractions;
using KwikNesta.Shared.Responses;
using KwikNesta.Shared.ServiceDTOs.Property;

namespace KwikNesta.Shared.ServiceQueries.Property
{
    public class ViewingCheckInPreviewQuery : IKNRequest<Response<ViewingRequestCheckInPreview>>
    {
        public Guid ViewingRequestId { get; set; }
        public string UserId { get; set; } = string.Empty;
    }
}