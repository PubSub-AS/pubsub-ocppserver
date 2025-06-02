using PubSub.OcppServer.Models.EF;
using System.Net.WebSockets;
using System.Security.Cryptography;
using PubSub.OcppServer.Data.Interfaces;
using System.Text;
using PubSub.OcppServer.Services.Interfaces;

namespace PubSub.OcppServer.Services
{
    public class BasicAuthSecurityHandler : ISecurityProfileHandler
    {
     
        private readonly ILogger<BasicAuthSecurityHandler> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public BasicAuthSecurityHandler(
            ILogger<BasicAuthSecurityHandler> logger,
            IUnitOfWork unitOfWork)
        {
      
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> AuthenticateAsync(HttpContext context)
        {
            
            // Handle Basic Authentication
            if (context.Request.Headers.TryGetValue("Authorization", out var authHeader))
            {
                return ValidateBasicAuth(authHeader);
            }

            return false;
        }

        private bool ValidateBasicAuth(string authHeader)
        {
            _logger.LogDebug("Received auth header");
            if (string.IsNullOrEmpty(authHeader) || !authHeader.StartsWith("Basic "))
            {
                return false;
            }
            string encodedCredentials = authHeader.Substring("Basic ".Length).Trim();
            string decodedCredentials;
            try
            {
                decodedCredentials = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(encodedCredentials));
            }
            catch
            {
                return false;
            }
            string[] parts = decodedCredentials.Split(':', 2);
            
            if (parts.Length != 2) return false;
            string username = parts[0];
            string password = parts[1];

            return VerifyCredentials(username, password) != null;
        }

        public User? VerifyCredentials(string username, string password)
        {
            var credentialsMatchingUsername = _unitOfWork
                .Users
                .Find(u =>
                    u.UserId == username);
            if (credentialsMatchingUsername == null) return null;
            if (!credentialsMatchingUsername.Any())
            {
                _logger.LogDebug("Invalid username or password");
                return null;
            }

            var credential = credentialsMatchingUsername.FirstOrDefault();
            var verified = VerifyPassword(
                password,
                credential.HashedPassword,
                credential.Salt);
            return credential;
        }

        bool VerifyPassword(string password, string hash, string saltString)
        {
            var salt = Convert.FromBase64String(saltString);
            var hashToCompare = Rfc2898DeriveBytes.Pbkdf2(
                password, 
                salt,
                1000,
                HashAlgorithmName.SHA512,
                64);
            var verified = CryptographicOperations.FixedTimeEquals(hashToCompare, Convert.FromBase64String(hash));
            return verified;
        }
        public string HashPassword(string password, out string saltString)
        {
            var salt = RandomNumberGenerator.GetBytes(64);
            var hash = Rfc2898DeriveBytes.Pbkdf2(
                Encoding.UTF8.GetBytes(password),
                salt,
                1000,
                HashAlgorithmName.SHA512,
                64);
            saltString = Convert.ToBase64String(salt);
            return Convert.ToBase64String(hash);
        }

        public CredentialsWithSalt GenerateRandomCredentialsWithSalt(string username)
        {
            var credentialsWithSalt = new CredentialsWithSalt() { Username = username };
            credentialsWithSalt.UnhashedPassword = CreateRandomPassword();
            var hashedPassword = HashPassword(credentialsWithSalt.UnhashedPassword, out var salt);
            credentialsWithSalt.HashedPassword = hashedPassword;
            credentialsWithSalt.Salt = salt;
            return credentialsWithSalt;
        }
        private static string CreateRandomPassword(int length = 32)
        {
            string validChars = "ABCDEFGHJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789@*_-=:+|";
            Random random = new Random();

            // Select one random character at a time from the string
            // and create an array of chars
            char[] chars = new char[length];
            for (int i = 0; i < length; i++)
            {
                chars[i] = validChars[random.Next(0, validChars.Length)];
            }
            return new string(chars);
        }

    }

    public class CredentialsWithSalt
    {
        public string Username { get; set; }
        public string UnhashedPassword { get; set; }
        public string HashedPassword { get; set; }
        public string Salt { get; set; }
    }
}
