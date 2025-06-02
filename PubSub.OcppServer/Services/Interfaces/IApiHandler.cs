using System.Security.Claims;
using PubSub.OcppServer.Models.Dtos;
using PubSub.OcppServer.Models.EF;
using PubSub.OcppServer.Models.Internal;

namespace PubSub.OcppServer.Services.Interfaces;

public interface IApiHandler
{
   
    List<ChargingPointDto> GetChargingPointsByFacility(string facilityName);
    List<ChargingPointDto> GetChargingPoints();
    List<FacilityDto>GetAuthorizedFacilities();
    List<string?> GetChargingPointIds();
    TransactionDto? GetTransactionById(string chargingTransactionId);
    IEnumerable<TransactionDto> GetAllTransactions();
    IEnumerable<TransactionDto> GetActiveTransactions();
    string GetUserById(string userId);
}