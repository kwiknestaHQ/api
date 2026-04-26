using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using System.Net;
using KwikNesta.Shared.Responses;

namespace KwikNestaGateway.API.Middlewares
{
    public class ErrorHandlerMiddleware(RequestDelegate next,
                                ILogger<ErrorHandlerMiddleware> logger)
    {
        private readonly RequestDelegate _next = next;
        private readonly ILogger<ErrorHandlerMiddleware> _logger = logger;

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex, _logger);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, 
                                                Exception ex, 
                                                ILogger<ErrorHandlerMiddleware> logger)
        {
            var message = string.Empty;
            switch (ex)
            {
                case Exception e:
                    logger.LogError(ex, ex.Message);
                    message = e.Message;
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    break;
            }

            var response = Response<object>.Fail(message, context.Response.StatusCode);
            context.Response.ContentType = "application/json";
            var result = JsonConvert.SerializeObject(response, new JsonSerializerSettings
            {
                ContractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new CamelCaseNamingStrategy()
                },
                Formatting = Formatting.Indented
            });
            await context.Response.WriteAsync(result);
        }
    }
}