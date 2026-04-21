namespace KwikNesta.Shared.Helpers
{
    public class UrlHelpers
    {
        public static string GetViewRequestResponseLink(string baseUrl, Guid requestId, string token)
        {
            return $"{baseUrl}/view-requests/{requestId}/respond?token={token}";
        }

        public static string GetVirtualViewRequestJoinLink(string baseUrl, Guid sessionId)
        {
            return $"{baseUrl}/view-sessions/{sessionId}";
        }

        public static string GetPhysicalViewRequestCheckInLink(string baseUrl, Guid requestId)
        {
            return $"{baseUrl}/view-requests/{requestId}/check-in";
        }
    }
}