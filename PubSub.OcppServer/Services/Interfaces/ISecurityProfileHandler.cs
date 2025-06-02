using PubSub.OcppServer.Models.EF;

namespace PubSub.OcppServer.Services.Interfaces
{
    public interface ISecurityProfileHandler
    {
        Task<bool> AuthenticateAsync(HttpContext context);
        User? VerifyCredentials(string username, string password);
        string HashPassword(string password, out string saltString);
        CredentialsWithSalt GenerateRandomCredentialsWithSalt(string username);
    }
}
