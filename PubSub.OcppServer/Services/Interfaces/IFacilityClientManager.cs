using PubSub.OcppServer.Controllers;
using PubSub.OcppServer.Models.Dtos;
using PubSub.OcppServer.Models.EF;
using PubSub.OcppServer.Models.Internal;

namespace PubSub.OcppServer.Services.Interfaces;

public interface IFacilityClientManager
{
    void RegisterHandler(int facilityId, Action<TransactionDto> handler);
    void UnregisterHandler(int facilityId, Action<TransactionDto> handler);
    void RouteMessage(int facilityId, ChargingTransaction message);

}