using KwikNesta.Shared.Models.Enumerations.Property;

namespace KwikNesta.Shared.ServiceDTOs.Property
{
    public class ViewRequestDto
    {
        public Guid Id { get; set; }
        public decimal Fee { get; set; }
        public ViewRequestPropertyDto Property { get; set; } = default!;
        public ViewRequesterDto Requester { get; set; } = default!;
        public DateTime RequestedDate { get; set; }
        public EViewingStatus Status { get; set; }
        public EViewingPaymentStatus PaymentStatus { get; set; }
        public DateTime? RespondedAt { get; set; }
        public EViewingType Type { get; set; }
        public string? Note { get; set; }
        public DateTime CreateAt { get; set; }
        public double ExpirationHours => 24 - (DateTime.UtcNow - CreateAt).TotalHours;
    }

    public class ViewRequesterDto
    {
        public string Id { get; set; } = default!;
        public string Name { get; set; } = default!;
        public string Phone { get; set; } = default!;
        public string Email { get; set; } = default!;
    }

    public class ViewRequestPropertyDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = default!;
        public decimal Price { get; set; }
        public string? Image { get; set; }
        public string Location { get; set; } = default!;
    }
}