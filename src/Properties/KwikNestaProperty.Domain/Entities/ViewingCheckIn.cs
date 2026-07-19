using KwikNesta.Shared.Models;

namespace KwikNestaProperty.Domain.Entities
{
    public class ViewingCheckIn : BaseEntity
    {
        public Guid ViewingRequestId { get; set; }
        public ViewingRequest ViewingRequest { get; set; } = null!;

        public string UserId { get; set; } = string.Empty;
        public double UserLatitude { get; set; }
        public double UserLongitude { get; set; }
        public double GpsAccuracyMeters { get; set; }
        public double DistanceMeters { get; set; }
        public string? IpAddress { get; set; }
    }
}