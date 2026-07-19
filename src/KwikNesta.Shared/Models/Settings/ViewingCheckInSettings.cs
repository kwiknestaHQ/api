namespace KwikNesta.Shared.Models.Settings
{
    public class ViewingCheckInSettings
    {
        public double ThresholdMeters { get; set; }
        public double MaxAccuracyMeters { get; set; }
        public int WindowMinutesBefore { get; set; }
        public int WindowMinutesAfter { get; set; }
    }
}