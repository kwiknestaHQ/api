namespace KwikNestaProperty.Application
{
    internal static class SessionJoinPolicy
    {
        private static readonly TimeSpan EarlyJoinWindow = TimeSpan.FromMinutes(2);
        private static readonly TimeSpan LateJoinCutoff = TimeSpan.FromSeconds(60 * 5);

        public static SessionAccessResult Evaluate(DateTime sessionStartUtc,
                                    DateTime sessionEndUtc)
        {
            var nowUtc = DateTime.UtcNow;
            var timeUntilStart = sessionStartUtc - nowUtc;
            if (timeUntilStart > EarlyJoinWindow)
                return SessionAccessResult.DeniedTooEarly(
                    $"Session hasn't started yet. You can join in {(int)timeUntilStart.TotalMinutes} minute(s).");

            var timeUntilEnd = sessionEndUtc - nowUtc;
            if (timeUntilEnd < LateJoinCutoff)
                return SessionAccessResult.DeniedTooLate(
                    timeUntilEnd.TotalSeconds <= 0
                        ? "This session has already ended."
                        : "This session is ending soon and is no longer accepting new participants.");

            return SessionAccessResult.Allowed();
        }
    }
}