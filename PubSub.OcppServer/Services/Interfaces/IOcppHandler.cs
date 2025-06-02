using PubSub.OcppServer.Models.Dtos;
using PubSub.OcppServer.Models.EF;
using PubSub.OcppServer.Models.FramingProtocol;
using PubSub.OcppServer.Models.Internal;
using PubSub.OcppServer.Models.Ocpp.v16;
using ResetTypeEnum = PubSub.OcppServer.Models.Internal.ResetTypeEnum;

namespace PubSub.OcppServer.Services.Interfaces;

public interface IOcppHandler
{
   
    string ChargingPointId { get; set; }
    OcppVersionEnum OcppVersion { get; }
    void HandleIncomingRequest(string rawMessage);


    Task<ApiResponseDto> CancelReservationAsync(int reservationId);
    Task<ApiResponseDto> ChangeConfigurationAsync(string configKey, string configValue);
    Task<ApiResponseDto> ClearCacheAsync();
    Task<ApiResponseDto> GetConfigurationAsync(List<string> configKeys);
    Task<ApiResponseDto> RemoteStartTransactionAsync(int connectorId, string idTag);
    Task<ApiResponseDto> RemoteOptimizedStartTransactionAsync(int connectorId, string idTag, OptimizedChargingArgs optimizedChargingArgs);
    Task<ApiResponseDto> RemoteStopTransactionAsync(int connectorId);
    Task<ApiResponseDto> ReserveNowAsync(int connectorId, DateTimeOffset expiryDate, string idTag);
    Task<ApiResponseDto> ResetChargingPointAsync(bool isHard);
    Task<ApiResponseDto> UnlockConnectorAsync(int connectorId);

}