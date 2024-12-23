using System.Security.Claims;
using Business.Interfaces;
using Microsoft.AspNetCore.Authorization;
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
        var jwtService = context.RequestServices.GetService<IJwtService>();

        var endpoint = context.GetEndpoint();

        if (endpoint?.Metadata.GetMetadata<AllowAnonymousAttribute>() != null)
        {
            await _next(context);
            return;
        }

        var authHeader = context.Request.Headers["Authorization"].FirstOrDefault();

        if (!string.IsNullOrEmpty(authHeader) && authHeader.StartsWith("Bearer "))
        {
            var token = authHeader.Substring(7);
            var claimsPrincipal = jwtService.VerifyAccessToken(token);

            if (claimsPrincipal != null)
            {
                context.User = claimsPrincipal;
                var userId = claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                context.Items["UserId"] = userId;
            }

            await _next(context);
            return;
        }

        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
        await context.Response.WriteAsJsonAsync(new { context.Response.StatusCode, Message = "Unauthorized" });
    }
}