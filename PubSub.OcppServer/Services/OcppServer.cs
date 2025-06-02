using System.Globalization;
using AutoMapper;
using PubSub.OcppServer.Data.Interfaces;
using PubSub.OcppServer.Models.Internal;
using PubSub.OcppServer.Models.EF;
using PubSub.OcppServer.Models.Ocpp.v201;
using MeterValue = PubSub.OcppServer.Models.EF.MeterValue;
using PubSub.OcppServer.Services.Interfaces;
using PubSub.OcppServer.Adapters;

namespace PubSub.OcppServer.Services
{


    public class OcppServer : IOcppServer
    {
     
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<OcppServer> _logger;
        private readonly IMapper _mapper; 
        private readonly IEnergyPriceAdapter _energyPriceService;
        private readonly IChargingProfileService _chargingProfileService;
        private readonly IFacilityClientManager _facilityClientManager;

        public OcppServer(
         
            IUnitOfWork unitOfWork,
            ILogger<OcppServer> logger,
            IMapper mapper,
            IEnergyPriceAdapter energyPriceService,
            IChargingProfileService chargingProfileService,
            IFacilityClientManager facilityClientManager)
        {
          
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
            _energyPriceService = energyPriceService;
            _chargingProfileService = chargingProfileService;
            _facilityClientManager = facilityClientManager;
        }


        public bool IsTransactionActive(string chargingTransactionId)
        {
           var transaction = _unitOfWork
                    .ChargingTransactions
                    .Find(c => c.ChargingTransactionID == chargingTransactionId)
                    .FirstOrDefault();
           if (transaction == null)  return false;
           return transaction.LastUpdated + TimeSpan.FromMinutes(30) >= DateTime.UtcNow;
        }
      
        public TransactionInternal GetChargingTransactionBy16Id(int chargingTransactionId)
        {
            var ct = _unitOfWork
                .ChargingTransactions
                .Find(c => c.v16Id == chargingTransactionId)
                .FirstOrDefault();
            return _mapper.Map<TransactionInternal>(ct);
        }

        public void SetTransactionState(string transactionId, string transactionState)
        {
            ChargingTransaction? transaction = _unitOfWork
                .ChargingTransactions
                .Find(ct => ct.ChargingTransactionID == transactionId)
                .FirstOrDefault();
            if(transaction != null) { return; }
            transaction.State = transactionState;
            _unitOfWork.Complete();
        }

        public void SetChargingPointState(string chargingPointId, string state)
        {
            // Charging point status
            var chargingPoint = _unitOfWork
                .ChargingPoints
                .Find(cp => cp.ChargingPointID == chargingPointId)
                .FirstOrDefault();
            if (chargingPoint == null)
            {
                _logger.LogCritical("Didn't find charge point in DB!");
                return;
            }
            chargingPoint.State = state;
            _unitOfWork.Complete();
        }
        public void SetConnectorState(string chargingPointId, int connectorId, string connectorState)
        {
            var connector = _unitOfWork
                .Connectors
                .Find(c => c.ChargingPointId == chargingPointId &&
                           c.ConnectorName == connectorId)
                .FirstOrDefault();
            if (connector == null)
            {
                _logger.LogCritical("Didn't find connector in db! Trying to create it.");
                _unitOfWork.Connectors.Add(new Connector()
                {
                    ChargingPointId = chargingPointId,
                    ConnectorName = connectorId,
                    ChargingTransactions = new List<ChargingTransaction>(),
                    State = connectorState
                });
                _unitOfWork.Complete();
                return;
            }
            connector.State = connectorState;
            _unitOfWork.Complete();
        }

        public async Task<Tuple<int, string>> StartTransactionAsync(TransactionInternal transaction)
        {
          
            int transIDv16 = 0;
            string transID = "";
            if (transaction.OcppVersion == OcppVersionEnum.v16)
            {
                transIDv16 =
                    _unitOfWork
                        .ChargingTransactions
                        .GetFirstAvailablev16Id();

            }
            transID = Guid.NewGuid().ToString();

            var chargingProfile =
                _chargingProfileService.Get(transaction.ChargingPointId, transaction.Connector.ConnectorId);
            if (chargingProfile != null)
            {
                foreach (var chargingSchedulePeriod in chargingProfile.ChargingSchedule.ChargingSchedulePeriod)
                {
                    var startSchedule = chargingProfile.ChargingSchedule.StartSchedule ?? DateTimeOffset.UtcNow;
                    _unitOfWork.ChargingSchedulePeriods.Add(new Models.EF.ChargingSchedulePeriod
                    {
                        ChargingTransactionId = transID,
                        ChargingSchedulePeriodId = Guid.NewGuid().ToString(),
                        StartTime = startSchedule + TimeSpan.FromSeconds(chargingSchedulePeriod.StartPeriod),
                        Charging = chargingSchedulePeriod.Limit > 0
                        
                    });
                }
            }

            var chargingTransactionEf = new ChargingTransaction
            {
                ChargingTransactionID = transID,
                v16Id = transIDv16,
                ConnectorName = transaction.Connector.ConnectorId,
                ChargingPointID = transaction.ChargingPointId,
                IdTagID = transaction.IdTagId,
                State = transaction.State.ToString(),
                LastUpdated = DateTimeOffset.UtcNow,
                MeterValues = new List<MeterValue>()
            };
            _unitOfWork.ChargingTransactions.Add(chargingTransactionEf);
            _unitOfWork.Complete();
            _facilityClientManager.RouteMessage(chargingTransactionEf.ChargingPoint.FacilityID, chargingTransactionEf);
            return new Tuple<int, string>(transIDv16, transID);
        }

     

        public async Task<bool> StopTransactionAsync(string transactionId)
        {
            var chargingTransactionEf = _unitOfWork
                .ChargingTransactions
                .Find(c => c.ChargingTransactionID == transactionId)
                .FirstOrDefault();
            if (chargingTransactionEf != null)
            {
                chargingTransactionEf.State = "Finished";
                chargingTransactionEf.LastUpdated = DateTimeOffset.UtcNow;
                chargingTransactionEf.EffectWatts = 0;
                 _facilityClientManager.RouteMessage(chargingTransactionEf.ChargingPoint.FacilityID, chargingTransactionEf);
            }
            _unitOfWork.Complete();
           
            return true;
        }

        public bool StoreMeterValues(List<MeterValueInternal> meterValues)
        {
            var transactionId = meterValues.First().ChargingTransactionID;

            var transaction = _unitOfWork.ChargingTransactions.GetChargingTransactionWithEnergyZoneName(transactionId);
            if (transaction == null)
            {
                _logger.LogDebug("Couldn't find transaction in database. Ignoring Metervalue");
                return false;
            }
            
            var transactionIdsFromMeterValues = new List<string>();
            foreach (var meterValue in meterValues)
            {
                transaction.LastUpdated = meterValue.Timestamp;
                if (meterValue.ValueRaw != null)
                {
                    if (meterValue.Measurand == "Power.Active.Import")
                        transaction.EffectWatts = (int) meterValue.ValueRaw;
                    if (meterValue.Measurand == "SoC")
                        transaction.Soc = (int) meterValue.ValueRaw;
                }

                _unitOfWork.Complete();
                transactionIdsFromMeterValues.Add(meterValue.ChargingTransactionID);
                var meterValueDb = _mapper.Map<MeterValue>(meterValue);

                if (meterValueDb.Unit == "kWh")
                {
                    var lastRegisteredKWh =
                        transaction.MeterValues
                            .Where(m => m.Unit == "kWh")
                            .Max(m => m.ValueRaw);
                    var chargedKwhSinceLastValue = lastRegisteredKWh != null ? meterValueDb.ValueRaw - lastRegisteredKWh : 0;
                    /*
                    meterValueDb.PriceEuros = _energyPriceService
                        .GetCurrentPriceMwhEuros(transaction.ChargingPoint.Facility.EnergyZoneName) * chargedKwhSinceLastValue / 1000;
                    */
                    meterValueDb.PriceEuros = 0; // Replace with the above if you have access to energy prices from ENTSOE-E or similar
                }
        
                transaction.MeterValues.Add(meterValueDb);
                
            }
            transaction.CalculateTotalKWh();
            transaction.CalculateTotalSeconds();
            transaction.CalculatePrice();
            _facilityClientManager.RouteMessage(transaction.ChargingPoint.FacilityID, transaction);

            _unitOfWork.Complete();

            foreach (var tid in transactionIdsFromMeterValues
                         .Select(transactionIdFromMeterValue => _unitOfWork
                            .ChargingTransactions
                            .Find(c => c.ChargingTransactionID == transactionIdFromMeterValue).FirstOrDefault()).Where(tid => tid != null))
            {
                if (tid!= null) tid.LastUpdated = DateTime.UtcNow;
            }
            _unitOfWork.Complete();
            return true;
        }

        public void StoreChargingPointInfo(string chargingPointId,
            string? chargePointSerialNumber,
            string? firmwareVersion,
            string? chargePointModel)
        {
            bool updatedDb = _unitOfWork.ChargingPoints
                .UpdateChargingPoint(chargingPointId, chargePointSerialNumber, firmwareVersion, chargePointModel);
            _unitOfWork.Complete();
            if (!updatedDb)
                _logger.LogInformation("Could not update chargingpoint " + chargingPointId + " in db.");
        }
        public bool IsVendorKnown(string vendorName)
        {
            var knownVendor = _unitOfWork
                .ChargingPointProviders
                .Find(p => p.ChargingPointProviderName == vendorName);
            return knownVendor.Any();
        }
        public bool IsIdTagValid(string? idTag)
        {
            // TODO: Add check for user
            var idTagInDb = _unitOfWork
                .IdTags
                .Find(i => i.IdTagID == idTag);
            return idTagInDb.Any();
        }
        public bool IsIdTokenValid(IdTokenType idToken)
        {
            // TODO: Add check for user
            var idTagInDb = _unitOfWork
                .IdTags
                .Find(i => i.IdTagID == idToken.IdToken);
            return idTagInDb.Any();
        }
    }

}
