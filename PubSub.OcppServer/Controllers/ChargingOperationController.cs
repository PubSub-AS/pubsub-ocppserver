using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PubSub.OcppServer.Models.Dtos;
using PubSub.OcppServer.Models.EF;
using PubSub.OcppServer.Models.Internal;
using PubSub.OcppServer.Services.Interfaces;

namespace PubSub.OcppServer.Controllers
{
    [ApiController]
    //[Authorize]
    public class ChargingOperationController : Controller
    {
        private readonly ILogger<ChargingOperationController> _logger;
        private readonly IOcppClientManager _ocppClientManager;
        private readonly IChargingProfileService _chargingProfileService;

        public ChargingOperationController(ILogger<ChargingOperationController> logger,
           
            IOcppClientManager ocppClientManager,
            IChargingProfileService chargingProfileService)
        {
            _logger = logger;
            _ocppClientManager = ocppClientManager;
            _chargingProfileService = chargingProfileService;
 
        }


        [HttpGet("/ocpp/chargingoperation/changeconfiguration/{chargingPointId}")]
        public async Task<ApiResponseDto> ChangeConfigurationAsync(string chargingPointId, string configKey, string configValue)
        {
            var ocppHandler = GetOcppHandler(chargingPointId);
            if (ocppHandler == null) return UnknownChargingPoint(chargingPointId);
            return await ocppHandler.ChangeConfigurationAsync(configKey, configValue);
        }

        [HttpGet("/ocpp/chargingoperation/getconfiguration/{chargingPointId}")]
        public async Task<ApiResponseDto> GetConfiguration(string chargingPointId, string[] configKeys)
        {
            var ocppHandler = GetOcppHandler(chargingPointId);
            if (ocppHandler == null) return UnknownChargingPoint(chargingPointId);
            return await ocppHandler.GetConfigurationAsync(configKeys.ToList());
        }




        [HttpGet("/ocpp/chargingoperation/remotestart/{chargingPointId}")]
        public async Task<ApiResponseDto> RemoteStartTransactionAsync(string chargingPointId, int connectorId, string idTag, OptimizedChargingArgs optimizedChargingArgs)
        {
            var ocppHandler = GetOcppHandler(chargingPointId);
            if (ocppHandler == null) return UnknownChargingPoint(chargingPointId);
            //var response = await ocppHandler.RemoteStartTransactionAsync(connectorId, idTag);
            var response = await ocppHandler.RemoteOptimizedStartTransactionAsync(connectorId, idTag, optimizedChargingArgs);
            return response;
        }
        [HttpGet("/ocpp/chargingoperation/remotestop/{chargingPointId}")]
        public async Task<ApiResponseDto> RemoteStopTransactionAsync(string chargingPointId, int connectorId)
        {
            var ocppHandler = GetOcppHandler(chargingPointId);
            if (ocppHandler == null) return UnknownChargingPoint(chargingPointId);
            var response = await ocppHandler.RemoteStopTransactionAsync(connectorId);
            return response;
        }
  
        [HttpGet("/ocpp/chargingoperation/reservenow/{chargingPointId}")]
        public async Task<ApiResponseDto> ReserveNow(string chargingPointId, int connectorId, DateTimeOffset expiryDate, string idTag)
        {
            var ocppHandler = GetOcppHandler(chargingPointId);
            if (ocppHandler == null) return UnknownChargingPoint(chargingPointId);
            return await ocppHandler.ReserveNowAsync(connectorId, expiryDate, idTag);
        }
        [HttpGet("/ocpp/chargingoperation/unlockconnector/{chargingPointId}")]
        public async Task<ApiResponseDto> UnlockConnectorAsync(string chargingPointId, int connectorId)
        {
            var ocppHandler = GetOcppHandler(chargingPointId);
            if (ocppHandler == null) return UnknownChargingPoint(chargingPointId);
            return await ocppHandler.UnlockConnectorAsync(connectorId);
        }
        private IOcppHandler? GetOcppHandler(string chargingPointId)
        {
            var version = _ocppClientManager.GetOcppVersion(chargingPointId);
            if (version == OcppVersionEnum.v16) return _ocppClientManager.Get16Handler(chargingPointId);
            if (version == OcppVersionEnum.v201) return _ocppClientManager.Get201Handler(chargingPointId);
            return null;
        }

        private ApiResponseDto UnknownChargingPoint(string chargingPointId)
        {
            return new ApiResponseDto()
            {
                Status = ApiResponseStatusEnum.ChargingPointNotConnected,
                StatusMessage = $"{chargingPointId} is unknown or not connected"
            };
        }

    }
}
