using System.Security.Claims;

namespace Business.Interfaces;

public interface IJwtService
{
    public string CreateJwtToken(string userId);

    public ClaimsPrincipal? VerifyAccessToken(string token);
}