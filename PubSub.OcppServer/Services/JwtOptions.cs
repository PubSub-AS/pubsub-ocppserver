namespace PubSub.OcppServer.Services;

public class JwtOptions
{
    public string JwtSecret { get; set; }
    public int AccessTokenValidityMinutes { get; set; }
    public int RefreshTokenValidityMinutes { get; set; }
}