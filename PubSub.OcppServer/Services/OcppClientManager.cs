using PubSub.OcppServer.Models.Internal;
using PubSub.OcppServer.Services.Interfaces;

namespace PubSub.OcppServer.Services
{
    public class OcppClientManager : IOcppClientManager
    {
        private Dictionary<string, IOcppHandler> _ocppHandlers = new();
        private readonly ILogger<OcppClientManager> _logger;
        public OcppClientManager(
            ILogger<OcppClientManager> logger)
        {
            _logger = logger;
        }
        public bool AddOcppHandler(string clientId, IOcppHandler ocppHandler)
        {
            if (_ocppHandlers.ContainsKey(clientId))
            {
                _ocppHandlers.Remove(clientId);
                _logger.LogInformation("Replacing ocpp handler for " + clientId);

            }
            ocppHandler.ChargingPointId = clientId;
            _ocppHandlers.Add(clientId, ocppHandler);
            _logger.LogInformation("Added ocppHandler: " + clientId);
            return true;
        }

        public bool RemoveOcppHandler(IOcppHandler ocppHandler)
        {
            if (!_ocppHandlers.ContainsValue(ocppHandler))
            {
                _logger.LogInformation("Attempt to remove OCPP handler "
                                       + ocppHandler.ChargingPointId
                                       + " failed. It did not exist.");
                return false;
            }
          
            _ocppHandlers.Remove(ocppHandler.ChargingPointId);
            _logger.LogInformation("Removed ocppHandler "
                                   + ocppHandler.ChargingPointId
                                   + " because the connection was closed");
            //_ocppServer.SetChargingPointState(ocppHandler.ChargingPointId, "Unavailable");
            return true;
        }

        public IOcppHandler? GetHandler(string chargingPointId)
        {
            var success = _ocppHandlers.TryGetValue(chargingPointId, out var handler);
            return success ? handler : null;
        }
        public OcppHandler16? Get16Handler(string chargingPointId)
        {
            var success = _ocppHandlers.TryGetValue(chargingPointId, out var handler);
            if (!success) return null;
            return handler.OcppVersion == OcppVersionEnum.v16 ? (OcppHandler16)handler : null;
        }
        public OcppHandler201? Get201Handler(string chargingPointId)
        {
            var success = _ocppHandlers.TryGetValue(chargingPointId, out var handler);
            if (!success) return null;
            return handler.OcppVersion == OcppVersionEnum.v201 ? (OcppHandler201)handler : null;
        }

        public bool IsHandlerActive(string chargingPointId)
        {
            return GetHandler(chargingPointId) != null;
        }

        public OcppVersionEnum GetOcppVersion(string chargingPointId)
        {
            var success = _ocppHandlers.TryGetValue(chargingPointId, out var handler);
            if (!success) return OcppVersionEnum.None;
            return handler.OcppVersion;
        }
    }
}
