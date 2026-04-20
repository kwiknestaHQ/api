namespace KwikNesta.Shared.Helpers
{
    public class UrlHelpers
    {
        public static string GetViewRequestResponseLink(string baseUrl, Guid requestId, string token)
        {
            return $"{baseUrl}/view-requests/{requestId}/respond?token={token}";
        }
    }
}
