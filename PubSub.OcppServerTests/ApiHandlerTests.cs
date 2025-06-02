using AutoMapper;
using Moq;
using Xunit;
using PubSub.OcppServer.Data.Interfaces;
using PubSub.OcppServer.Models.EF;
using PubSub.OcppServer.Models.Dtos;
using PubSub.OcppServer.Services;
using PubSub.OcppServer.Services.Interfaces;
using PubSub.OcppServer.Models.Ocpp.v16;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Linq.Expressions;
using PubSub.OcppServer.Models.Internal;

namespace PubSub.OcppServerTests
{
    public class ApiHandlerTests
    {
        private readonly ApiHandler _apiHandler;
        private readonly Mock<IOcppClientManager> _ocppClientManagerMock;
        private readonly Mock<ILogger<ApiHandler>> _loggerMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IMapper> _mapperMock;

        public ApiHandlerTests()
        {
            _ocppClientManagerMock = new Mock<IOcppClientManager>();
            _loggerMock = new Mock<ILogger<ApiHandler>>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _mapperMock = new Mock<IMapper>();

            _apiHandler = new ApiHandler(
                _ocppClientManagerMock.Object,
                _loggerMock.Object,
                _unitOfWorkMock.Object,
                _mapperMock.Object
            );
        }



        [Fact]
        public void GetTransactionById_ReturnsMappedTransactionDto()
        {
            // Arrange
            var transactionId = "123";
            var transaction = new ChargingTransaction { ChargingTransactionID = transactionId };

            _unitOfWorkMock
                .Setup(u => u.ChargingTransactions.GetTransactionAsync(transactionId))
                .ReturnsAsync(transaction);

            var transactionDto = new TransactionDto { ChargingTransactionId = transactionId };

            _mapperMock
                .Setup(m => m.Map<TransactionDto>(transaction))
                .Returns(transactionDto);

            // Act
            var result = _apiHandler.GetTransactionById(transactionId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(transactionId, result.ChargingTransactionId);
            _unitOfWorkMock.Verify(u => u.Complete(), Times.Once);
        }

        [Fact]
        public void GetCurrentTransactions_ReturnsMappedTransactions()
        {
            // Arrange
            var currentTransactions = new List<ChargingTransaction>
            {
                new() { ChargingTransactionID = "TX2", LastUpdated = DateTimeOffset.Now }
            }.AsQueryable();

            _unitOfWorkMock
                .Setup(u => u.ChargingTransactions.GetRecentTransactions())
                .Returns(currentTransactions);

            var mappedTransactions = new List<TransactionDto>
            {
                new() { ChargingTransactionId = "TX2" }
            };

            _mapperMock
                .Setup(m => m.Map<List<TransactionDto>>(It.IsAny<IEnumerable<ChargingTransaction>>()))
                .Returns(mappedTransactions);

            // Act
            var result = _apiHandler.GetAllTransactions();

            // Assert
            Assert.NotNull(result);
            Assert.Single(result);
        }

 

 

    }
}