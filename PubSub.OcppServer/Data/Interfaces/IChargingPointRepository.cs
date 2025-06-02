using PubSub.OcppServer.Models.EF;

namespace PubSub.OcppServer.Data.Interfaces
{
    public interface IChargingPointRepository : IGenericRepository<ChargingPoint>
    {
        List<ChargingPoint> GetChargingPoints();
        Dictionary<string, string> GetChargingPointsWithEnergyZone();
        bool UpdateChargingPoint(
            string chargingPointId,
            string? chargePointSerialNumber,
            string? firmwareVersion,
            string? chargePointModel);
    }
}
