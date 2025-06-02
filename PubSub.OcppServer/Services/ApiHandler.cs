
using AutoMapper;
using PubSub.OcppServer.Data.Interfaces;
using PubSub.OcppServer.Models.Dtos;
using PubSub.OcppServer.Models.EF;
using PubSub.OcppServer.Models.Ocpp.v16;
using PubSub.OcppServer.Models.Internal;
using ResetTypeEnum = PubSub.OcppServer.Models.Internal.ResetTypeEnum;
using PubSub.OcppServer.Services.Interfaces;

namespace PubSub.OcppServer.Services
{
    public class ApiHandler : IApiHandler
    {
        private readonly IOcppClientManager _ocppClientManager;
        private readonly ILogger<ApiHandler> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
       

        public ApiHandler(IOcppClientManager ocppClientManager, 
            ILogger<ApiHandler> logger, 
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _ocppClientManager = ocppClientManager;
            _logger = logger;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        
        public List<ChargingPointDto> GetChargingPointsByFacility(string facilityName)
        {
            var chargingPoints = _unitOfWork
                .ChargingPoints
                .GetAll()
                .Where(c => c.Facility.FacilityName == facilityName)
                .ToList();
            return _mapper.Map<List<ChargingPointDto>>(chargingPoints);
        }

        public TransactionDto? GetTransactionById(string chargingTransactionId)
        {
            
            var transaction =
                _unitOfWork
                    .ChargingTransactions
                    .GetTransactionAsync(chargingTransactionId)
                    .Result;
            _unitOfWork.Complete();
            return transaction == null ? null : _mapper.Map<TransactionDto>(transaction);
        }

        public IEnumerable<TransactionDto> GetAllTransactions()
        {
            var currentTransactions = _unitOfWork
                .ChargingTransactions
                .GetRecentTransactions();

            return _mapper.Map<List<TransactionDto>>(currentTransactions);
        }
        public IEnumerable<TransactionDto> GetActiveTransactions()
        {
            // until further notice hardcoded as max 8 hours since last update
            var allTransactions = GetAllTransactions();
            return allTransactions.Where(t => DateTimeOffset.UtcNow - t.LastUpdated < TimeSpan.FromHours(8));
        }

        public List<ChargingPointDto> GetChargingPoints()
        {
            var chargingPoints = _unitOfWork
                .ChargingPoints
                .GetChargingPoints();
            return _mapper.Map<List<ChargingPointDto>>(chargingPoints);
        }
        public List<FacilityDto> GetAuthorizedFacilities()
        {
            var facilities = _unitOfWork
                .Facilities
                .GetAuthorizedFacilities();
                
            return _mapper.Map<List<FacilityDto>>(facilities);
        }
        public List<string?> GetChargingPointIds()
        {
            var chargingPointIds = GetChargingPoints()
                    .Select(c => c.ChargingPointID);
            return chargingPointIds.ToList();
        }

        public string GetUserById(string userId)
        {
            var userFromDb = _unitOfWork.ApiUsers.Find(u => u.UserId == userId);
            return !(userFromDb.Any()) ? "" : userFromDb.FirstOrDefault().Email;
        }
    }
}
