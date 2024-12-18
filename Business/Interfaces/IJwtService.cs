using System.Security.Claims;

namespace Business.Interfaces;

public interface IJwtService
{
    public string CreateJwtToken(int userId);

    public ClaimsPrincipal? VerifyAccessToken(string token);
}