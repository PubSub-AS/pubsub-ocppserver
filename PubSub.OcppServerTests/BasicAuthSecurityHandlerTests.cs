using Moq;
using Xunit;
using Microsoft.Extensions.Logging;
using PubSub.OcppServer.Services;
using PubSub.OcppServer.Data.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System.Text;
using System.Security.Cryptography;
using System.Linq;
using System.Collections.Generic;
using PubSub.OcppServer.Models.EF;
using System.Linq.Expressions;

namespace PubSub.OcppServerTests
{
    public class BasicAuthSecurityHandlerTests
    {
        // We can keep the logger mock since it doesn't change across tests
        private readonly Mock<ILogger<BasicAuthSecurityHandler>> _loggerMock;

        public BasicAuthSecurityHandlerTests()
        {
            _loggerMock = new Mock<ILogger<BasicAuthSecurityHandler>>();
        }

        // Helper method to create a mock IUnitOfWork
        private Mock<IUnitOfWork> CreateUnitOfWorkMock()
        {
            return new Mock<IUnitOfWork>();
        }

        // Helper method to set up HttpContext with Authorization header
        private HttpContext CreateHttpContextWithAuthHeader(string authHeader)
        {
            var context = new DefaultHttpContext();
            context.Request.Headers["Authorization"] = authHeader;
            return context;
        }

        [Fact]
        public async Task AuthenticateAsync_ReturnsFalse_WhenNoAuthHeaderIsPresent()
        {
            // Arrange
            var context = new DefaultHttpContext(); // No Authorization header
            var unitOfWorkMock = CreateUnitOfWorkMock();
            var handler = new BasicAuthSecurityHandler(_loggerMock.Object, unitOfWorkMock.Object);

            // Act
            var result = await handler.AuthenticateAsync(context);

            // Assert
            Assert.False(result); // Should return false if no auth header is present
        }

        [Fact]
        public async Task AuthenticateAsync_ReturnsFalse_WhenInvalidAuthHeaderIsPresent()
        {
            // Arrange
            var context = CreateHttpContextWithAuthHeader("Invalid Header");
            var unitOfWorkMock = CreateUnitOfWorkMock();
            var handler = new BasicAuthSecurityHandler(_loggerMock.Object, unitOfWorkMock.Object);

            // Act
            var result = await handler.AuthenticateAsync(context);

            // Assert
            Assert.False(result); // Should return false for invalid headers
        }

        [Fact]
        public async Task AuthenticateAsync_ReturnsFalse_WhenMalformedBasicAuthHeader()
        {
            // Arrange
            var invalidCredentials = Convert.ToBase64String(Encoding.UTF8.GetBytes("malformedheader"));
            var context = CreateHttpContextWithAuthHeader($"Basic {invalidCredentials}");
            var unitOfWorkMock = CreateUnitOfWorkMock();
            var handler = new BasicAuthSecurityHandler(_loggerMock.Object, unitOfWorkMock.Object);

            // Act
            var result = await handler.AuthenticateAsync(context);

            // Assert
            Assert.False(result); // Should return false for malformed credentials
        }

        [Fact]
        public async Task AuthenticateAsync_ReturnsTrue_WhenValidCredentialsAreProvided()
        {
            // Arrange
            var username = "testuser";
            var password = "testpassword";
            var validCredentials = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{username}:{password}"));
            var context = CreateHttpContextWithAuthHeader($"Basic {validCredentials}");

            // Set up a fresh UnitOfWork mock for this test
            var unitOfWorkMock = CreateUnitOfWorkMock();
            var handler = new BasicAuthSecurityHandler(_loggerMock.Object, unitOfWorkMock.Object);

            // Mock the user data in UnitOfWork to return matching credentials
            var testUser = new User
            {
                UserId = username,
                HashedPassword = Convert.ToBase64String(Rfc2898DeriveBytes.Pbkdf2(password, new byte[64], 1000, HashAlgorithmName.SHA512, 64)),
                Salt = Convert.ToBase64String(new byte[64])
            };

            // Use Expression<Func<User, bool>> in the mock setup
            unitOfWorkMock.Setup(u => u.Users.Find(It.IsAny<Expression<Func<User, bool>>>()))
                          .Returns(new List<User> { testUser }.AsQueryable());

            // Act
            var result = await handler.AuthenticateAsync(context);

            // Assert
            Assert.True(result); // Should return true for valid credentials
        }

        [Fact]
        public async Task AuthenticateAsync_ReturnsFalse_WhenInvalidCredentialsAreProvided()
        {
            // Arrange
            var username = "testuser";
            var password = "wrongpassword";
            var validCredentials = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{username}:{password}"));
            var context = CreateHttpContextWithAuthHeader($"Basic {validCredentials}");

            // Set up a fresh UnitOfWork mock for this test
            var unitOfWorkMock = CreateUnitOfWorkMock();
            var handler = new BasicAuthSecurityHandler(_loggerMock.Object, unitOfWorkMock.Object);

            // Mock the user data in UnitOfWork to return a user with a different password
            var testUser = new User
            {
                UserId = username,
                HashedPassword = Convert.ToBase64String(Rfc2898DeriveBytes.Pbkdf2("rightpassword", new byte[64], 1000, HashAlgorithmName.SHA512, 64)),
                Salt = Convert.ToBase64String(new byte[64])
            };

            // Use Expression<Func<User, bool>> in the mock setup
            unitOfWorkMock.Setup(u => u.Users.Find(It.IsAny<Expression<Func<User, bool>>>()))
                          .Returns(new List<User> { }.AsQueryable());

            // Act
            var result = await handler.AuthenticateAsync(context);

            // Assert
            Assert.False(result); // Should return false for incorrect credentials
        }
    }
}
