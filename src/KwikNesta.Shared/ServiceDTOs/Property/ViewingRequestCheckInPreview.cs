namespace KwikNesta.Shared.ServiceDTOs.Property
{
    public class ViewingRequestCheckInPreview
    {
        public Guid Id { get; set; }
        public ViewingRequestCheckInPreviewProperty Property { get; set; } = default!;
        public DateTime ScheduledAt { get; set; }
        public DateTime WindowStart { get; set; }
        public DateTime WindowEnd { get; set; }
        public bool AlreadyCheckeIn { get; set; }
        public double ThresholdMeters { get; set; }
        public double MaxAccuracyMeters { get; set; }

    }

    public class ViewingRequestCheckInPreviewProperty
    {
        public string Address { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string PhotoUrl { get; set; } = string.Empty;
    }
}