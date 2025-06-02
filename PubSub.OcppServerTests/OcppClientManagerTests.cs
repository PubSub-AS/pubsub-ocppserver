using Moq;
using Xunit;
using Microsoft.Extensions.Logging;
using PubSub.OcppServer.Services;
using PubSub.OcppServer.Services.Interfaces;
using System.Collections.Generic;

namespace PubSub.OcppServerTests
{
    public class OcppClientManagerTests
    {
        private readonly OcppClientManager _clientManager;
        private readonly Mock<ILogger<OcppClientManager>> _loggerMock;

        public OcppClientManagerTests()
        {
            // Create a mock logger
            _loggerMock = new Mock<ILogger<OcppClientManager>>();

            // Instantiate the client manager with the mock logger
            _clientManager = new OcppClientManager(_loggerMock.Object);
        }

        [Fact]
        public void AddOcppHandler_ShouldAddNewHandler_WhenHandlerDoesNotExist()
        {
            // Arrange
            var clientId = "client1";
            var ocppHandlerMock = new Mock<IOcppHandler>();

            // Act
            var result = _clientManager.AddOcppHandler(clientId, ocppHandlerMock.Object);

            // Assert
            Assert.True(result);
            var handler = _clientManager.GetHandler(clientId);
            Assert.Equal(ocppHandlerMock.Object, handler);
        }

        [Fact]
        public void AddOcppHandler_ShouldReplaceExistingHandler_WhenHandlerAlreadyExists()
        {
            // Arrange
            var clientId = "client1";
            var firstHandlerMock = new Mock<IOcppHandler>();
            var secondHandlerMock = new Mock<IOcppHandler>();

            // Add the first handler
            _clientManager.AddOcppHandler(clientId, firstHandlerMock.Object);

            // Act
            var result = _clientManager.AddOcppHandler(clientId, secondHandlerMock.Object);

            // Assert
            Assert.True(result);
            var handler = _clientManager.GetHandler(clientId);
            Assert.Equal(secondHandlerMock.Object, handler); // The second handler should replace the first one

        }

        [Fact]
        public void RemoveOcppHandler_ShouldRemoveExistingHandler()
        {
            // Arrange
            var clientId = "client1";
            var ocppHandlerMock = new Mock<IOcppHandler>();
            ocppHandlerMock.SetupGet(h => h.ChargingPointId).Returns(clientId);

            // Add the handler first
            _clientManager.AddOcppHandler(clientId, ocppHandlerMock.Object);

            // Act
            var result = _clientManager.RemoveOcppHandler(ocppHandlerMock.Object);

            // Assert
            Assert.True(result);
            var handler = _clientManager.GetHandler(clientId);
            Assert.Null(handler); // Handler should be removed

        }

        [Fact]
        public void RemoveOcppHandler_ShouldReturnFalse_WhenHandlerDoesNotExist()
        {
            // Arrange
            var ocppHandlerMock = new Mock<IOcppHandler>();
            ocppHandlerMock.SetupGet(h => h.ChargingPointId).Returns("client1");

            // Act
            var result = _clientManager.RemoveOcppHandler(ocppHandlerMock.Object);

            // Assert
            Assert.False(result);}

        [Fact]
        public void GetHandler_ShouldReturnNull_WhenHandlerDoesNotExist()
        {
            // Act
            var handler = _clientManager.GetHandler("nonexistent");

            // Assert
            Assert.Null(handler);
        }

        [Fact]
        public void GetHandler_ShouldReturnHandler_WhenHandlerExists()
        {
            // Arrange
            var clientId = "client1";
            var ocppHandlerMock = new Mock<IOcppHandler>();

            // Add the handler
            _clientManager.AddOcppHandler(clientId, ocppHandlerMock.Object);

            // Act
            var handler = _clientManager.GetHandler(clientId);

            // Assert
            Assert.Equal(ocppHandlerMock.Object, handler);
        }
    }
}
