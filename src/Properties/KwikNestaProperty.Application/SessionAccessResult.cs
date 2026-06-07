namespace KwikNestaProperty.Application
{
    public class SessionAccessResult
    {
        public bool TooEarly { get; set; }
        public bool TooLate { get; set; }
        public bool CanJoin { get; set; }
        public string? Reason { get; init; }

        public static SessionAccessResult Allowed() => new() { CanJoin = true };
        public static SessionAccessResult DeniedTooEarly(string reason) => new() { TooEarly = true, Reason = reason };
        public static SessionAccessResult DeniedTooLate(string reason) => new() { TooLate = true, Reason = reason };
    }
}