namespace KwikNesta.Shared.ServiceDTOs.Property
{
    public class ViewingCheckInResponse
    {
        public Guid Id { get; set; }
        public DateTime CheckedInAt { get; set; }
        public double DistanceMeters { get; set; }
    }
}