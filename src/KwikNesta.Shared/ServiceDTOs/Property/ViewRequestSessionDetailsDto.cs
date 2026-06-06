namespace KwikNesta.Shared.ServiceDTOs.Property
{
    public class ViewRequestSessionDetailsDto
    {
        public Guid Id { get; set; } = default!;
        public string PropertyAddress { get; set; } = default!;
        public string PropertyPhoto { get; set; } = default!;
        public string PropertyType { get; set; } = default!;
        public DateTime ScheduledAt { get; set; }
        public int EstimatedDurationMinutes { get; set; }
        public string InspectionType { get; set; } = default!;
        public string Status { get; set; } = default!;
    }
}
