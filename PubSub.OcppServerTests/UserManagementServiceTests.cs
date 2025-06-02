using Moq;
using Xunit;
using PubSub.OcppServer.Services;
using PubSub.OcppServer.Data.Interfaces;
using PubSub.OcppServer.Services.Interfaces;
using PubSub.OcppServer.Models.EF;
using PubSub.OcppServer.Models.Dtos;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.Extensions.Logging;

namespace PubSub.OcppServerTests
{
    public class UserManagementServiceTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IJwtTokenService> _jwtTokenServiceMock;
        private readonly UserManagementService _service;

        public UserManagementServiceTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _jwtTokenServiceMock = new Mock<IJwtTokenService>();
            _jwtTokenServiceMock.Setup(s => s.Settings)
                .Returns(new JwtOptions()
                {
                    AccessTokenValidityMinutes = 1,
                    JwtSecret = "test",
                    RefreshTokenValidityMinutes = 33
                });

            var basicAuthSecurityHandler = new BasicAuthSecurityHandler(
                Mock.Of<ILogger<BasicAuthSecurityHandler>>(), // Mock ILogger
                _unitOfWorkMock.Object); // Mock IUnitOfWork

            var securityHandlers = new List<ISecurityProfileHandler>
            {
                basicAuthSecurityHandler
            };

            _service = new UserManagementService(
                _unitOfWorkMock.Object,
                _jwtTokenServiceMock.Object,
                securityHandlers);

        
        }

        [Fact]
        public void CreateUser_ShouldAddUser_WhenUserDoesNotExist()
        {
            // Arrange
            var userInfo = new UserInfoWithPasswordDto
            {
                UserId = "test@test.com",
                Password = "password123",
                FirstName = "Test",
                LastName = "User"
            };

            _unitOfWorkMock.Setup(u => u.ApiUsers.Find(It.IsAny<Expression<Func<ApiUser, bool>>>()))
                .Returns(Enumerable.Empty<ApiUser>().AsQueryable());

      
            // Act
            var result = _service.CreateUser(userInfo);

            // Assert
            Assert.True(result);
            _unitOfWorkMock.Verify(u => u.ApiUsers.Add(It.IsAny<ApiUser>()), Times.Once);
            _unitOfWorkMock.Verify(u => u.Complete(), Times.Once);
        }

        [Fact]
        public void CreateUser_ShouldNotAddUser_WhenUserExists()
        {
            // Arrange
            var userInfo = new UserInfoWithPasswordDto
            {
                UserId = "test@test.com",
                Password = "password123",
                FirstName = "Test",
                LastName = "User"
            };

            _unitOfWorkMock.Setup(u => u.ApiUsers.Find(It.IsAny<Expression<Func<ApiUser, bool>>>()))
                .Returns(new List<ApiUser> { new ApiUser { Email = "test@test.com" } }.AsQueryable());

            // Act
            var result = _service.CreateUser(userInfo);

            // Assert
            Assert.False(result);
            _unitOfWorkMock.Verify(u => u.ApiUsers.Add(It.IsAny<ApiUser>()), Times.Never);
            _unitOfWorkMock.Verify(u => u.Complete(), Times.Never);
        }

        [Fact]
        public void VerifyUserEmailExists_ShouldReturnTrue_WhenUserExists()
        {
            // Arrange
            var user = new UserInfoWithPasswordDto { UserId = "test@test.com" };

            _unitOfWorkMock.Setup(u => u.ApiUsers.Find(It.IsAny<Expression<Func<ApiUser, bool>>>()))
                .Returns(new List<ApiUser> { new ApiUser { Email = "test@test.com" } }.AsQueryable());

            // Act
            var result = _service.VerifyUserEmailExists(user);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void VerifyUserEmailExists_ShouldReturnFalse_WhenUserDoesNotExist()
        {
            // Arrange
            var user = new UserInfoWithPasswordDto { UserId = "test@test.com" };

            _unitOfWorkMock.Setup(u => u.ApiUsers.Find(It.IsAny<Expression<Func<ApiUser, bool>>>()))
                .Returns(Enumerable.Empty<ApiUser>().AsQueryable());

            // Act
            var result = _service.VerifyUserEmailExists(user);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void RemoveRefreshToken_ShouldClearRefreshToken_WhenUserExists()
        {
            // Arrange
            var userWithToken = new ApiUser { RefreshToken = "oldToken" };

            _unitOfWorkMock.Setup(u => u.ApiUsers.Find(It.IsAny<Expression<Func<ApiUser, bool>>>()))
                .Returns(new List<ApiUser> { userWithToken }.AsQueryable());

            // Act
            _service.RemoveRefreshToken("oldToken");

            // Assert
            Assert.Equal(string.Empty, userWithToken.RefreshToken);
            _unitOfWorkMock.Verify(u => u.Complete(), Times.Once);
        }

      
    }
}
