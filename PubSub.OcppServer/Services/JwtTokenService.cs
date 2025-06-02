using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PubSub.OcppServer.Services.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace PubSub.OcppServer.Services
{
    public class JwtTokenService : IJwtTokenService
    {
        public JwtTokenService(IOptions<JwtOptions> jwtSettings)
        {
            Settings = jwtSettings.Value;
        }

        public JwtOptions Settings { get; }

        public string GenerateAccessToken(List<Claim> claims)
        {
            
            var jwtToken = new JwtSecurityToken(
                claims: claims,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddMinutes(Settings.AccessTokenValidityMinutes),
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(Settings.JwtSecret)
                    ),
                    SecurityAlgorithms.HmacSha256Signature)
            );
            return new JwtSecurityTokenHandler().WriteToken(jwtToken);
        }
        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }
    }
}
