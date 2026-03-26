using System.Diagnostics;

namespace KwikNestaGateway.API.Middlewares
{
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestLoggingMiddleware> _logger;

        public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var sw = Stopwatch.StartNew();

            await _next(context);

            sw.Stop();

            var request = context.Request;
            var response = context.Response;
            var endpoint = context.GetEndpoint()?.DisplayName ?? "UnknownEndpoint";

            _logger.LogInformation(
                "HTTP {Method} {Path} responded {StatusCode} in {Elapsed}ms | Endpoint: {Endpoint}",
                request.Method,
                request.Path,
                response.StatusCode,
                sw.Elapsed.TotalMilliseconds,
                endpoint
            );
        }
    }
}
