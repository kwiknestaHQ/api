namespace KwikNesta.Shared.ServiceDTOs.Infra
{
    public class AuditLogResponseDto
    {
        public string UserName { get; set; } = default!;
        public string Action { get; set; } = default!;
        public string? IpAddress { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}