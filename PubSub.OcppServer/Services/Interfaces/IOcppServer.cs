using PubSub.OcppServer.Models.Dtos;
using PubSub.OcppServer.Models.EF;
using PubSub.OcppServer.Models.EventArguments;
using PubSub.OcppServer.Models.Internal;
using PubSub.OcppServer.Models.Ocpp.v201;

namespace PubSub.OcppServer.Services.Interfaces
{
    public interface IOcppServer
    {

        TransactionInternal GetChargingTransactionBy16Id(int chargingTransactionId);
        void SetChargingPointState(string chargingPointId, string state);
        void SetTransactionState(string transactionId, string transactionState);
        void SetConnectorState(string chargingPointId, int connectorId, string connectorState);
        Task<Tuple<int, string>> StartTransactionAsync(TransactionInternal transaction);

        Task<bool> StopTransactionAsync(string transactionId);

        bool IsTransactionActive(string chargingTransactionId);

        bool StoreMeterValues(List<MeterValueInternal> meterValues);
        // void CreateGenericEvent(string chargingPointId, GenericEventTypeEnum eventType);
        void StoreChargingPointInfo(string chargingPointId,
            string? chargePointSerialNumber,
            string? firmwareVersion,
            string? chargePointModel);

        bool IsVendorKnown(string vendorName);
        bool IsIdTagValid(string? idTag);
        bool IsIdTokenValid(IdTokenType idToken);
    }
}
