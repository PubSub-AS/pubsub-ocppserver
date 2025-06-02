using PubSub.OcppServer.Data.Interfaces;
using PubSub.OcppServer.Models.EF;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

namespace PubSub.OcppServer.Data
{
    public class ChargingPointRepository : GenericRepository<ChargingPoint>, IChargingPointRepository
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly string _email;
        private readonly bool _isAdmin;
        public ChargingPointRepository(
            ChargingContext context,
            IHttpContextAccessor httpContextAccessor) : base(context)
        {
            _httpContextAccessor = httpContextAccessor;
            _email = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.Email);
            _isAdmin = _httpContextAccessor.HttpContext?.User.Claims.FirstOrDefault(c => c.Type == "IsAdmin")?.Value == "True";
        }

        public List<ChargingPoint> GetChargingPoints()
        {
            var chargingPoints = _context
                .ChargingPoints
                .AsNoTracking()
                .Include(c => c.Facility)
                .Where(c => c.ApiUsers.FirstOrDefault(a => a.Email == _email) != null);
            return chargingPoints.ToList();
        }

        public Dictionary<string, string> GetChargingPointsWithEnergyZone()
        {
            return _context
                .ChargingPoints
                .Include(c => c.Facility)
               
                .Where(c => c.ChargingPointID != null)
                .ToDictionary(c => c.ChargingPointID, c => c
                    .Facility
                    .EnergyZoneName);
        }
        public bool UpdateChargingPoint(
            string chargingPointId,
            string? chargePointSerialNumber,
            string? firmwareVersion,
            string? chargePointModel)
        {
            var chargingPoint =
                _context
                    .ChargingPoints
                    .FirstOrDefault(c => c.ChargingPointID == chargingPointId);
            if (chargingPoint == null) { return  false; }
            chargingPoint.ChargePointSerialNumber = chargePointSerialNumber;
            chargingPoint.FirmwareVersion = firmwareVersion;
            chargingPoint.ChargePointModel = chargePointModel;
            _context.SaveChanges();
            return true;
        }
    }
}
