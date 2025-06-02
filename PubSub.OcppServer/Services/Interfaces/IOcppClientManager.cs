using PubSub.OcppServer.Models.Internal;

namespace PubSub.OcppServer.Services.Interfaces;

public interface IOcppClientManager
{
    bool AddOcppHandler(string clientId, IOcppHandler ocppHandler);
    bool RemoveOcppHandler(IOcppHandler ocppHandler);
    IOcppHandler? GetHandler(string chargingPointId);
    OcppHandler16? Get16Handler(string chargingPointId);
    OcppHandler201? Get201Handler(string chargingPointId);
    bool IsHandlerActive(string chargingPointId);
    OcppVersionEnum GetOcppVersion(string chargingPointId);
}