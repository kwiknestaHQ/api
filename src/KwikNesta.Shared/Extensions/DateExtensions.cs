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
    }
}