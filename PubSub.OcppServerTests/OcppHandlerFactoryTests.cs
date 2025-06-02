/*
using Moq;
using Microsoft.Extensions.DependencyInjection;
using PubSub.OcppServer.Models.FramingProtocol;
using PubSub.OcppServer.Models.Internal;

using PubSub.OcppServer.Services;
using PubSub.OcppServer.Services.Interfaces;
using Xunit;
using Microsoft.Extensions.Logging;
using PubSub.OcppServer.Data.Interfaces;

namespace PubSub.OcppServerTests
{
    public class OcppHandlerFactoryTests
    {
        private readonly Mock<IServiceProvider> _serviceProviderMock;
        private readonly OcppHandler16 _ocppHandler;
        private readonly Mock<IOcppMessageSerializer> _messageParserMock;
        private readonly Mock<ILogger<OcppHandler16>> _loggerMock;
        private readonly Mock<ISendMessageBus> _sendMessageBusMock;
        private readonly Mock<IOcppClientManager> _clientManagerMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IOcppRequestManager> _requestManagerMock;
        private readonly Mock<IOcppMessageDispatcher> _messageDispatcherMock;
        private readonly Mock<IChargingProfileService> _chargingProfileServiceMock;
        private readonly Mock<IOcppServer> _ocppServerMock;
        private readonly OcppHandlerContext _context;

        public OcppHandlerFactoryTests()
        {
            _serviceProviderMock = new Mock<IServiceProvider>();
            _messageParserMock = new Mock<IOcppMessageSerializer>();
            _loggerMock = new Mock<ILogger<OcppHandler16>>();
            _sendMessageBusMock = new Mock<ISendMessageBus>();
            _clientManagerMock = new Mock<IOcppClientManager>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _requestManagerMock = new Mock<IOcppRequestManager>();
            _messageDispatcherMock = new Mock<IOcppMessageDispatcher>();
            _chargingProfileServiceMock = new Mock<IChargingProfileService>();
            _ocppServerMock = new Mock<IOcppServer>();
            _context = new OcppHandlerContext();
            // Mock TaskCompletionSource to avoid hanging
            var mockResponse = new OcppResponseOrError();
            _requestManagerMock
                .Setup(rm => rm.CreateAndSendPendingRequest(It.IsAny<Call>(), It.IsAny<string>(), It.IsAny<Type>()))
                .ReturnsAsync(mockResponse); // Return an immediately completed task

            _context = new OcppHandlerContext()
            {
                ChargingPointId = "TESTPOINT",
                Connectors = new List<ConnectorInternal>()
            };
            _ocppHandler = new OcppHandler16(
                _messageParserMock.Object,
                _loggerMock.Object,
                _ocppServerMock.Object,
                //_sendMessageBusMock.Object,
                _clientManagerMock.Object,
                _unitOfWorkMock.Object,
                _context,
                _requestManagerMock.Object,
                _messageDispatcherMock.Object,
                _chargingProfileServiceMock.Object
            );
        }

        [Fact]
        public void CreateHandler_ShouldReturnOcpp16Handler_ForVersion16()
        {
            // Arrange
            var ocpp16Handler = _ocppHandler;
            _serviceProviderMock
                .Setup(sp => sp.GetService(typeof(OcppHandler16)))
                .Returns(ocpp16Handler);

            var factory = new OcppHandlerFactory(_serviceProviderMock.Object);

            // Act
            var handler = factory.CreateHandler(OcppVersionEnum.v16);

            // Assert
            Assert.NotNull(handler);
            Assert.IsType<OcppHandler16>(handler);
        }
        /*
        [Fact]
        public void CreateHandler_ShouldReturnOcpp201Handler_ForVersion201()
        {
            // Arrange
            var ocpp201Handler = new Ocpp201Handler();
            _serviceProviderMock
                .Setup(sp => sp.GetService(typeof(Ocpp201Handler)))
                .Returns(ocpp201Handler);

            var factory = new OcppHandlerFactory(_serviceProviderMock.Object);

            // Act
            var handler = factory.CreateHandler(OcppVersionEnum.v201);

            // Assert
            Assert.NotNull(handler);
            Assert.IsType<Ocpp201Handler>(handler);
        }
        
        [Fact]
        public void CreateHandler_ShouldThrowArgumentException_ForInvalidVersion()
        {
            // Arrange
            var factory = new OcppHandlerFactory(_serviceProviderMock.Object);

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() =>
                factory.CreateHandler((OcppVersionEnum)999));
            Assert.Equal("Invalid OCPP protocol version (Parameter 'ocppVersion')", exception.Message);
        }
    }
}
*/
