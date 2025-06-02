using System.Text.Json;
using Microsoft.Extensions.Logging;
using Moq;
using PubSub.OcppServer.Models.FramingProtocol;
using PubSub.OcppServer.Models.Ocpp.v16;
using PubSub.OcppServer.Services;
using Xunit;

namespace PubSub.OcppServerTests
{
    public class OcppMessageSerializerTests
    {
        private readonly OcppMessageSerializer _ocppMessageParser;
        private readonly Mock<ILogger<OcppMessageSerializer>> _mockLogger;

        public OcppMessageSerializerTests()
        {
            _mockLogger = new Mock<ILogger<OcppMessageSerializer>>();
            _ocppMessageParser = new OcppMessageSerializer(_mockLogger.Object);
        }

        /*
        [Fact]
        public void GetOcppMessage_ValidCallMessage_ReturnsParsedOcppMessage()
        {
            // Arrange
            var frameMessage = new FrameMessage("[2,\"1234\",\"Authorize\",{\"idTag\":\"ABC123\"}]");

            // Act
            var result = _ocppMessageSerializer.GetOcppMessage(frameMessage);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<AuthorizeRequest>(result);
            var authorizeRequest = result as AuthorizeRequest;
            Assert.Equal("ABC123", authorizeRequest.IdTag);
        }
        
        [Fact]
        public void GetOcppMessage_InvalidCallMessage_ReturnsEmptyOcppMessage()
        {
            // Arrange
            var frameMessage = new FrameMessage("[2,\"1234\",\"UnknownAction\",{}]");

            // Act
            var result = _ocppMessageSerializer.GetOcppMessage(frameMessage);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<OcppMessage>(result);
        }
        
        [Fact]
        public void SerializeFrameRequest_ReturnsSerializedJson()
        {
            // Arrange
            var uniqueId = "1234";
            var requestPayload = new { idTag = "ABC123" };

            // Act
            var result = _ocppMessageSerializer.SerializeCall(uniqueId, requestPayload);

            // Assert
            var expectedJson = "[2,\"1234\",{\"idTag\":\"ABC123\"}]";
            Assert.Equal(expectedJson, result);
        }
        */
        [Fact]
        public void SerializeFrameResponse_ReturnsSerializedJson()
        {
            // Arrange
            var uniqueId = "1234";
            var responsePayload = new { status = "Accepted" };

            // Act
            var result = _ocppMessageParser.SerializeCallResult(uniqueId, responsePayload);

            // Assert
            var expectedJson = "[3,\"1234\",{\"status\":\"Accepted\"}]";
            Assert.Equal(expectedJson, result);
        }


    }
}
