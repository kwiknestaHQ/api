using KwikNesta.Shared.Extensions;
using KwikNesta.Shared.Models.Enumerations.Identity;
using KwikNesta.Shared.Responses;
using KwikNestaIdentity.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.Text.Json;

namespace KwikNestaGateway.API.Middlewares
{
    public class ActiveUserMiddleware
    {
        private readonly RequestDelegate _next;

        public ActiveUserMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, UserManager<User> userManager)
        {
            var user = context.User;

            if (user?.Identity?.IsAuthenticated == true)
            {
                var userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                if (!string.IsNullOrWhiteSpace(userId))
                {
                    var dbUser = await userManager.FindByIdAsync(userId);

                    if (dbUser == null || dbUser.Status != EUserStatus.Active)
                    {
                        context.Response.StatusCode = StatusCodes.Status403Forbidden;
                        context.Response.ContentType = "application/json";
                        var status = dbUser == null ? "deactivated" : dbUser.Status.GetDescription().ToLower();
                        var json = JsonSerializer.Serialize(Response<string>.Fail($"Account is {status}.", 403));
                        await context.Response.WriteAsync(json);
                        return;
                    }
                }
            }

            await _next(context);
        }
    }
}