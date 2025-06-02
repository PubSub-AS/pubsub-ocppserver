using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PubSub.OcppServer.Models.Dtos;
using PubSub.OcppServer.Services.Interfaces;

namespace PubSub.OcppServer.Controllers
{
    //[Authorize]
    [ApiController]
    public class ChargingPointController : Controller
    {
        private readonly ILogger<ChargingPointController> _logger;
        private readonly IApiHandler _apiHandler;

        public ChargingPointController(ILogger<ChargingPointController> logger,
            IApiHandler apiHandler)
        {
            _logger = logger;
            _apiHandler = apiHandler;
        }

        [HttpGet("/ocpp/chargingpoints")]
        public List<ChargingPointDto> GetChargingPoints()
        {
            var req = HttpContext.Request;
            var chargingPoints = _apiHandler.GetChargingPoints();
            return chargingPoints;
        }

    }
}