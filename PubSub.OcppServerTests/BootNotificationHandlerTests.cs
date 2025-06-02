using Moq;
using Xunit;
using PubSub.OcppServer.Services;
using PubSub.OcppServer.Models.Ocpp.v16;
using Microsoft.Extensions.Logging;
using PubSub.OcppServer.Services.Interfaces;
using PubSub.OcppServer.OcppMessageIncomingHandlers.v16;

namespace PubSub.OcppServerTests
{
    public class BootNotificationHandlerTests
    {
        private readonly Mock<IOcppServer> _ocppServerMock;
        private readonly Mock<ILogger<BootNotificationIncomingHandler>> _loggerMock;
        private readonly OcppHandlerContext _context;
        private readonly BootNotificationIncomingHandler _incomingHandler;

        public BootNotificationHandlerTests()
        {
            _ocppServerMock = new Mock<IOcppServer>();
            _loggerMock = new Mock<ILogger<BootNotificationIncomingHandler>>();
            _context = new OcppHandlerContext { ChargingPointId = "test-charging-point" };
            _incomingHandler = new BootNotificationIncomingHandler(_ocppServerMock.Object, _loggerMock.Object, _context);
        }

        [Fact]
        public void Handle_ShouldStoreChargingPointInfoAndReturnAccepted()
        {
            // Arrange
            var request = new BootNotificationRequest
            {
                ChargePointSerialNumber = "serial123",
                ChargePointModel = "modelX",
                FirmwareVersion = "v1.0.0"
            };

            // Act
            var result = _incomingHandler.Handle(request);

            // Assert
            Assert.Equal(RegistrationStatus.Accepted, result.Status);
            _ocppServerMock.Verify(x => x.StoreChargingPointInfo(
                "test-charging-point",
                request.ChargePointSerialNumber,
                request.FirmwareVersion,
                request.ChargePointModel
            ), Times.Once);
        }
    }
}