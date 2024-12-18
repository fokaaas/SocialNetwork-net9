using System.Security.Claims;
using Business.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Business.Middlewares;

public class JwtAuthorizationMiddleware
{
    private readonly RequestDelegate _next;

    public JwtAuthorizationMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        var jwtService = context.RequestServices.GetRequiredService<JwtService>();
        var authHeader = context.Request.Headers["Authorization"].FirstOrDefault();

        if (!string.IsNullOrEmpty(authHeader) && authHeader.StartsWith("Bearer "))
        {
            var token = authHeader.Substring(7);
            var claimsPrincipal = jwtService.VerifyAccessToken(token);

            if (claimsPrincipal != null)
            {
                context.User = claimsPrincipal;
                var userId = claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                context.Items["userId"] = userId;
            }
        }

        await _next(context);
    }
}