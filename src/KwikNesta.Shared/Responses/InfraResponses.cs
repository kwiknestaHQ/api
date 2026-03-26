namespace KwikNesta.Shared.Responses
{
    public static class InfraResponses
    {
        public static readonly string LocationDataloadCompletedSubject = "Location Data Load Completed Successfully";
        public static readonly string LocationDataloadCompletedMessage = "This is to inform you that the location data migration you initiated has been completed.<br><br><strong>Summary:</strong><ul><li><strong>Status:</strong> Completed Successfully</li><li><strong>Environment:</strong> {0}</li><li><strong>Source IP Address:</strong> {1}</li><li><strong>Start Time:</strong> {2}</li><li><strong>Duration:</strong> {3}</li></ul>You may now proceed with any dependent operations.<br>If this action was not performed by you, please report it immediately.";
        public static readonly string RecordNotFound = "The requested resource not found.";
        public static readonly string CountryToggled = "{0} successfully {1}";
    }
}
