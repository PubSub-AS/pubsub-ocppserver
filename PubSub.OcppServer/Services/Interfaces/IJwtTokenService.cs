using System.Security.Claims;

namespace PubSub.OcppServer.Services.Interfaces;

public interface IJwtTokenService
{
    JwtOptions Settings { get; }
    string GenerateAccessToken(List<Claim> claims);
    string GenerateRefreshToken();
}