namespace KwikNesta.Shared.ServiceDTOs.Payment
{
    public class PayoutAccountDto
    {
        public Guid Id { get; set; }
        public string UserId { get; set; } = default!;
        public DateTime CreatedOn { get; set; }
        public DateTime? LastUpdatedOn { get; set; }
        public string AccountName { get; set; } = default!;
        public string AccountNumber { get; set; } = default!;
        public string BankCode { get; set; } = default!;
        public string BankName { get; set; } = default!;
        public string RecipientCode { get; set; } = default!;
        public bool IsActive { get; set; }
    }
}