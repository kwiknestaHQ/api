namespace KwikNesta.Shared.Extensions
{
    public static class DateExtensions
    {
        public static string FormatDate(this DateTime date, 
                                    string format = "dd MMM, yyyy HH:mm:ss 'GMT'zzz")
        {
            return date.ToString(format);
        }

        public static string FormatDuration(this DateTime start, DateTime end)
        {
            var duration = end - start;
            return $"{(int)duration.TotalHours}hr " +
                    $"{duration.Minutes}mins " +
                    $"{duration.Seconds}secs";
        }

        public static string FormatAsWat(this DateTime utcDateTime, 
                                    string format = "dddd, dd MMMM yyyy 'at' h:mm tt",
                                    bool includeWatVerbiage = true)
        {
            if (utcDateTime.Kind != DateTimeKind.Utc)
                utcDateTime = DateTime.SpecifyKind(utcDateTime, DateTimeKind.Utc);

            var watTime = utcDateTime.AddHours(1);

            return includeWatVerbiage ? 
                watTime.ToString(format) + " (WAT)" : 
                watTime.ToString(format);
        }

        public static DateTime ToUtcDateTime(this long ts)
        {
            return DateTimeOffset.FromUnixTimeSeconds(ts).UtcDateTime;
        }
    
    }
}