using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Shared.Middlewares;

public class UserIdMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context)
    {
        if (context.User.Identity.IsAuthenticated)
        {
            var userIdClaim = context.User.FindFirst(ClaimTypes.NameIdentifier);

            context.Items["UserId"] = userIdClaim != null ? Guid.Parse(userIdClaim.Value) : null;
        }

        await next(context);
    }
}