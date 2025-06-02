using Moq;
using Xunit;
using PubSub.OcppServer.Services;
using PubSub.OcppServer.Models.Ocpp.v16;
using Microsoft.Extensions.Logging;
using PubSub.OcppServer.Services.Interfaces;
using PubSub.OcppServer.OcppMessageIncomingHandlers.v16;

namespace PubSub.OcppServerTests
{
    public class AuthorizeHandlerTests
    {
        private readonly Mock<IOcppServer> _ocppServerMock;
        private readonly Mock<ILogger<AuthorizeIncomingHandler>> _loggerMock;
        private readonly OcppHandlerContext _context;
        private readonly AuthorizeIncomingHandler _incomingHandler;

        public AuthorizeHandlerTests()
        {
            _ocppServerMock = new Mock<IOcppServer>();
            _loggerMock = new Mock<ILogger<AuthorizeIncomingHandler>>();
            _context = new OcppHandlerContext { ChargingPointId = "test-charging-point" };
            _incomingHandler = new AuthorizeIncomingHandler(_ocppServerMock.Object, _loggerMock.Object, _context);
        }

        [Fact]
        public void Handle_ShouldReturnAcceptedResponse_WhenIdTagIsValid()
        {
            // Arrange
            var request = new AuthorizeRequest { IdTag = "valid-id-tag" };
            _ocppServerMock.Setup(x => x.IsIdTagValid(It.IsAny<string>())).Returns(true);

            // Act
            var result = _incomingHandler.Handle(request);

            // Assert
            Assert.Equal(AuthorizationStatus.Accepted, result.IdTagInfo.Status);
            _ocppServerMock.Verify(x => x.IsIdTagValid(It.IsAny<string>()), Times.Once);

        }

        [Fact]
        public void Handle_ShouldReturnInvalidResponse_WhenIdTagIsInvalid()
        {
            // Arrange
            var request = new AuthorizeRequest { IdTag = "invalid-id-tag" };
            _ocppServerMock.Setup(x => x.IsIdTagValid(It.IsAny<string>())).Returns(false);

            // Act
            var result = _incomingHandler.Handle(request);

            // Assert
            Assert.Equal(AuthorizationStatus.Invalid, result.IdTagInfo.Status);
            _ocppServerMock.Verify(x => x.IsIdTagValid(It.IsAny<string>()), Times.Once);

        }
    }
}
