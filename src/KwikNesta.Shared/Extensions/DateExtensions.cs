namespace KwikNesta.Shared.Extensions
{
    public static class DateExtensions
    {
        public static string FormatDate(this DateTime date, 
                                    string format = "dd MMM, yyyy HH:mm:ss 'GMT'zzz")
        {
            return date.ToString(format);
        }

        public static string FormatDureation(this DateTime start, DateTime end)
        {
            var duration = end - start;
            return $"{(int)duration.TotalHours}hr " +
                    $"{duration.Minutes}mins " +
                    $"{duration.Seconds}secs";
        }

        public static string FormatAsWat(this DateTime utcDateTime)
        {
            if (utcDateTime.Kind != DateTimeKind.Utc)
                utcDateTime = DateTime.SpecifyKind(utcDateTime, DateTimeKind.Utc);

            var watTime = utcDateTime.AddHours(1); // UTC +1

            return watTime.ToString("dddd, dd MMMM yyyy 'at' h:mm tt") + " (WAT)";
        }
    }
}