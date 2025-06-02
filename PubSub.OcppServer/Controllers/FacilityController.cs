using Microsoft.AspNetCore.Mvc;
using PubSub.OcppServer.Models.Dtos;
using PubSub.OcppServer.Services.Interfaces;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;

namespace PubSub.OcppServer.Controllers
{
    [ApiController]
    public class FacilityController : Controller
    {
        private readonly ILogger<FacilityController> _logger;
        private readonly IFacilityClientManager _facilityClientManager;
        private readonly IApiHandler _apiHandler;
        private WebSocket? _webSocket;
        public FacilityController(
            ILogger<FacilityController> logger, 
            IFacilityClientManager facilityClientManager,
            IApiHandler apiHandler)
        {
            _logger = logger;
            _facilityClientManager = facilityClientManager;
            _apiHandler = apiHandler;
        }

        [HttpGet("/ocpp/facilities/facilities")]
        public ActionResult<List<FacilityDto>> GetFacilities()
        {
            var facilities = _apiHandler.GetAuthorizedFacilities();
            return Ok(facilities);
        }
        [HttpGet("/ocpp/facilities/facility/{facilityId}")]
        public ActionResult<List<FacilityDto>> GetFacility(int facilityId)
        {
            var facilities = _apiHandler.GetAuthorizedFacilities();
            return Ok(facilities.FirstOrDefault(f=>f.FacilityID == facilityId));
        }

        [HttpGet("/ocpp/facilities/facility/ws/{facilityId}")]
        public async Task<IActionResult> GetWebSocket(int facilityId)
        {
            //var facilities = _apiHandler.GetAuthorizedFacilities();
            //if (!facilities.Select(f => f.FacilityID).Contains(facilityId))
              //  return Unauthorized("Unknown or unauthorized facility");
            if (!HttpContext.WebSockets.IsWebSocketRequest)
                return BadRequest("Expected Websocket request");
            _facilityClientManager.RegisterHandler(5, SendWebsocketMessage);
            _webSocket = await HttpContext
                .WebSockets
                .AcceptWebSocketAsync();
            if (_webSocket == null)
            {
                _logger.LogInformation("Websocket is not instantiated");
                return Empty;
            }
            try
            {

                var requestBuffer = new byte[1024 * 32];
                var receiveResult = await _webSocket.ReceiveAsync(new ArraySegment<byte>(requestBuffer), CancellationToken.None);

                while (!receiveResult.CloseStatus.HasValue)
                {
                    receiveResult = await _webSocket.ReceiveAsync(new ArraySegment<byte>(requestBuffer), CancellationToken.None);
                    _logger.LogInformation("Received an unexpected websocket message. What do do with it?: " + receiveResult);
                }

                _logger.LogDebug("Closing socket. CloseStatus: " + receiveResult.CloseStatus.Value + ", " + receiveResult.CloseStatusDescription);
            }
            catch (Exception ex)
            {
                // Log the exception
                _logger.LogError(ex, "An error occurred while handling the WebSocket connection");
                if (_webSocket.State == WebSocketState.Open)
                {
                    await _webSocket.CloseAsync(WebSocketCloseStatus.InternalServerError,
                        "An internal server error occurred",
                        CancellationToken.None);
                }
            }
            finally
            {
                if (_webSocket.State is WebSocketState.Open or WebSocketState.CloseReceived)
                {
                    await _webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closing connection", CancellationToken.None);
                }
                _facilityClientManager.UnregisterHandler(facilityId, SendWebsocketMessage);
            }
            return new EmptyResult();
        }
        private void SendWebsocketMessage(TransactionDto transactionMessage)
        {
            if (_webSocket == null) return;
            var messageString = JsonSerializer.Serialize(
                transactionMessage, new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                });
            var buffer = Encoding.UTF8.GetBytes(messageString);
            _webSocket.SendAsync(
                new ArraySegment<byte>(buffer, 0, messageString.Length),
                WebSocketMessageType.Text,
                true,
                CancellationToken.None);
            _logger.LogInformation("Facility event message sent: " + messageString);
        }

    }
}
