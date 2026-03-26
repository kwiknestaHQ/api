namespace KwikNesta.Shared.Models
{
    public class EmailWithAttachmentRequest : EmailRequest
    {
        public List<EmailAttachementsModel> Attachments { get; set; } = [];
    }

    public class EmailRequest
    {
        public string From { get; set; } = default!;
        public string To { get; set; } = default!;
        public string Subject { get; set; } = default!;
        public string Text { get; set; } = default!;
        public string Html { get; set; } = default!;
    }

    public class EmailAttachementsModel
    {
        public string Filename { get; set; } = default!;
        public string Content { get; set; } = default!;
        public string Content_type { get; set; } = default!;
    }
}