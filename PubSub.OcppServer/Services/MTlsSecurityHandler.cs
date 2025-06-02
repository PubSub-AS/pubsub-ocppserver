using PubSub.OcppServer.Services.Interfaces;
using System.Net.WebSockets;
using System.Security.Cryptography.X509Certificates;
using PubSub.OcppServer.Models.EF;

namespace PubSub.OcppServer.Services
{
    public class MTlsSecurityHandler : ISecurityProfileHandler
    {
        public async Task<bool> AuthenticateAsync(HttpContext context)
        {
            // Handle mTLS Authentication (retrieve and validate client certificate)
            var clientCertHeader = context.Request.Headers["X-ARR-ClientCert"].FirstOrDefault();
            if (!string.IsNullOrEmpty(clientCertHeader))
            {
                var clientCertBytes = Convert.FromBase64String(clientCertHeader);
                var clientCertificate = new X509Certificate2(clientCertBytes);

                // Implement certificate validation
                return ValidateClientCertificate(clientCertificate);
            }

            return false;
        }

        public User? VerifyCredentials(string username, string password)
        {
            throw new NotImplementedException();
        }

        public string HashPassword(string password, out string saltString)
        {
            throw new NotImplementedException();
        }

        private bool ValidateClientCertificate(X509Certificate2 clientCertificate)
        {
            // Validate the client certificate (e.g., verify against trusted CA)
            return clientCertificate.Verify(); // Return true if valid, otherwise false
        }

        public CredentialsWithSalt GenerateRandomCredentialsWithSalt(string username)
        {
            throw new NotImplementedException();
        }
    }

}
