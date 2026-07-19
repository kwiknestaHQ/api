using KwikNesta.Mediator.Cores.Abstractions;
using KwikNesta.Shared.Models;
using KwikNesta.Shared.Responses;
using KwikNesta.Shared.ServiceDTOs.Property;

namespace KwikNesta.Shared.ServiceCommands.Property
{
    public class ViewingCheckInCommand : IKNRequest<Response<ViewingCheckInResponse>>
    {
        public Guid ViewingRequestId { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double AccuracyMeters { get; set; }
        public UserContext? UserContext { get; set; }
    }

    public class ViewingCheckInRequest
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double AccuracyMeters { get; set; }
    }
}