using Hangfire.Dashboard;
using Microsoft.Extensions.Primitives;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http.Headers;
using System.Text;

namespace KwikNestaGateway.API.Filters
{
    public class HangfireAuthFilter : IDashboardAuthorizationFilter
    {
        private readonly string userName;
        private readonly string password;
        public HangfireAuthFilter(string user, string pass)
        {
            userName = user;
            password = pass;
        }

        public bool Authorize([NotNull] DashboardContext context)
        {
            HttpContext httpContext = context.GetHttpContext();
            StringValues stringValues = httpContext.Request.Headers["Authorization"];
            if (string.IsNullOrEmpty(stringValues))
            {
                SetResponse(httpContext);
                return false;
            }

            AuthenticationHeaderValue authenticationHeaderValue = AuthenticationHeaderValue.Parse(stringValues!);
            if (!"Basic".Equals(authenticationHeaderValue.Scheme, StringComparison.InvariantCultureIgnoreCase))
            {
                SetResponse(httpContext);
                return false;
            }

            string[] array = Encoding.UTF8.GetString(Convert.FromBase64String(authenticationHeaderValue.Parameter!)).Split(':');
            string text = array[0];
            string text2 = array[1];
            if (string.IsNullOrWhiteSpace(text) || string.IsNullOrWhiteSpace(text2))
            {
                SetResponse(httpContext);
                return false;
            }

            if (text2 == password && text == userName)
            {
                return true;
            }

            SetResponse(httpContext);
            return false;
        }

        private static void SetResponse(HttpContext httpContext)
        {
            httpContext.Response.StatusCode = 401;
            httpContext.Response.Headers.Append("WWW-Authenticate", "Basic realm=\"Hangfire Dashboard\"");
            httpContext.Response.WriteAsync("Authentication is required");
        }
    }
}
