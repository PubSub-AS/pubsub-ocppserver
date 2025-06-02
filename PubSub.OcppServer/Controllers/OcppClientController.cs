using System.Net.WebSockets;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PubSub.OcppServer.Data.Interfaces;
using PubSub.OcppServer.Models.EF;
using PubSub.OcppServer.Models.EventArguments;
using PubSub.OcppServer.Models.FramingProtocol;
using PubSub.OcppServer.Models.Internal;
using PubSub.OcppServer.Services;
using PubSub.OcppServer.Services.Interfaces;

namespace PubSub.OcppServer.Controllers;
[ApiController]
public class OcppClientController : ControllerBase
{
  
    private readonly ILogger<OcppClientController> _logger;
    private OcppVersionEnum _ocppVersion;
    private readonly IOcppHandlerFactory _ocppHandlerFactory;
    private readonly IOcppServer _ocppServer;
    private readonly IOcppClientManager _ocppClientManager;
    private IOcppHandler? _ocppHandler;
    private readonly IOcppMessageDispatcher _ocppMessageDispatcher;
    private string _chargingPointId;
    private readonly ISendMessageBus _sendMessageBus;
    private readonly IEnumerable<ISecurityProfileHandler> _securityProfileHandlers;
    private WebSocket? _webSocket;
    public event EventHandler<ConnectionArgs>? ConnectionStatusChanged;


    public OcppClientController(
        ILogger<OcppClientController> logger,
        IOcppHandlerFactory ocppHandlerFactory,
        IOcppMessageDispatcher ocppMessageDispatcher,
        IOcppServer ocppServer,
        IOcppClientManager ocppClientManager,
        ISendMessageBus sendMessageBus,
        IEnumerable<ISecurityProfileHandler> securityProfileHandlers)
    {
       
        _logger = logger;
        _ocppHandlerFactory = ocppHandlerFactory;
        _ocppMessageDispatcher = ocppMessageDispatcher;
        _ocppServer = ocppServer;
        _ocppClientManager = ocppClientManager;
        _sendMessageBus = sendMessageBus;
        _securityProfileHandlers = securityProfileHandlers;

        _sendMessageBus.OnSendMessage += SendMessageDelegate;
    }

    [HttpGet("/ocpp/ws/{chargingPointId}")]
    public async Task<IActionResult> Get(string chargingPointId)
    {
        _chargingPointId = chargingPointId;
        if(!HttpContext.WebSockets.IsWebSocketRequest)
            return BadRequest("Expected Websocket request");
        ISecurityProfileHandler selectedHandler = SelectSecurityProfileHandler(HttpContext);
        if (!await selectedHandler.AuthenticateAsync(HttpContext))
            return Unauthorized("Authentication failed");
        _logger.LogInformation($"ChargingPoint {_chargingPointId} successfully authorized.");
        SetOcppVersionFromSubProtocol(HttpContext.WebSockets.WebSocketRequestedProtocols);
        if (_ocppVersion == OcppVersionEnum.None)
        {
            _webSocket = await HttpContext
                .WebSockets
                .AcceptWebSocketAsync();
            await _webSocket
                .CloseAsync(WebSocketCloseStatus.ProtocolError, "Could not offer requested subprotocol.", CancellationToken.None);
            return Ok();
        }
               
        var subProtocol = _ocppVersion == OcppVersionEnum.v201 ? "ocpp2.0.1" : "ocpp1.6";

        // Log the response headers before upgrading to WebSocket
        foreach (var header in HttpContext.Response.Headers)
        {
            _logger.LogDebug($"{header.Key}: {header.Value}");
        }

        _webSocket = await HttpContext
            .WebSockets
            .AcceptWebSocketAsync(subProtocol)
            ;

        await HandleAsync();
        return new EmptyResult();
    }



   private void SetOcppVersionFromSubProtocol(IList<string> requestedSubProtocols)
    {
        if (requestedSubProtocols == null || !requestedSubProtocols.Any())
        {
            _logger.LogInformation("No subprotocols requested. Failing.");
            _ocppVersion = OcppVersionEnum.None;
            return;
        }
        if (requestedSubProtocols.Contains("ocpp2.0.1"))
        {
            _ocppVersion = OcppVersionEnum.v201;
            return;
        }
        if (requestedSubProtocols.Contains("ocpp1.6"))
        {
            _ocppVersion = OcppVersionEnum.v16;
            return;
        }

        _logger.LogInformation("Requested subprotocol is not supported.");
        _ocppVersion = OcppVersionEnum.None;

    }
    private ISecurityProfileHandler SelectSecurityProfileHandler(HttpContext context)
    {
        // Logic to select the correct security profile handler
        // Example: check a custom header or query parameter to determine the security profile
        if (context.Request.Headers["X-Security-Profile"] == "mTLS")
        {
            return _securityProfileHandlers.OfType<MTlsSecurityHandler>().FirstOrDefault();
        }
        return _securityProfileHandlers.OfType<BasicAuthSecurityHandler>().FirstOrDefault();
        
    }
    private async Task HandleAsync()
    {
        if (_webSocket == null)
        {
            _logger.LogInformation("Websocket is not instantiated");
            return;
        }
        try
        {
            _ocppHandler = _ocppHandlerFactory.CreateHandler(_ocppVersion);

            var isAlreadyConnected = !RegisterChargingPoint();
            if (isAlreadyConnected)
            {
                _logger.LogInformation("Charging point " + _chargingPointId + " reconnected.");
                await _webSocket.CloseAsync(WebSocketCloseStatus.EndpointUnavailable,
                    "Charging point ID already in use",
                    CancellationToken.None);
                return;
            } 

            var requestBuffer = new byte[1024 * 32];
            var responseBuffer = new byte[1024 * 32];
            var receiveResult = await _webSocket.ReceiveAsync(new ArraySegment<byte>(requestBuffer), CancellationToken.None);
          
            while (!receiveResult.CloseStatus.HasValue)
            {
                if (receiveResult.Count > 0)
                {
                    var receivedMessage = Encoding.UTF8.GetString(requestBuffer, 0, receiveResult.Count);
                    await _ocppMessageDispatcher.HandleIncomingOcppMessage(receivedMessage, _chargingPointId);
                }
         
                receiveResult = await _webSocket.ReceiveAsync(new ArraySegment<byte>(requestBuffer), CancellationToken.None);
            }

            _logger.LogDebug("Closing socket. CloseStatus: " + receiveResult.CloseStatus.Value + ", " + receiveResult.CloseStatusDescription);
        }
        catch (Exception ex)
        {
            // Log the exception
            _logger.LogError(ex, "An error occurred while handling the WebSocket connection for charging point " + _chargingPointId);
            if (_webSocket.State == WebSocketState.Open)
            {
                await _webSocket.CloseAsync(WebSocketCloseStatus.InternalServerError,
                    "An internal server error occurred",
                    CancellationToken.None);
            }
        }
        finally
        {
            if (_webSocket.State == WebSocketState.Open || _webSocket.State == WebSocketState.CloseReceived)
            {
                await _webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closing connection", CancellationToken.None);
            }

            ConnectionStatusChanged?.Invoke(this, new ConnectionArgs
            {
                ConnectionStatus = ConnectionStatusEnum.Closed
            });

            CloseConnection();
        }
    }
    private bool RegisterChargingPoint()
    {
  
        _ocppServer.SetChargingPointState(_chargingPointId, "Online");
        return _ocppClientManager.AddOcppHandler(_chargingPointId, _ocppHandler);

    }
    private void CloseConnection()
    {
        _ocppClientManager.RemoveOcppHandler(_ocppHandler);
        _ocppServer.SetChargingPointState(_chargingPointId, "Offline");
    }
    private void SendMessageDelegate(object sender, OcppResponseArgs e)
    {
        if (_webSocket == null) return;
        var buffer = Encoding.UTF8.GetBytes(e.OcppResponseMessage);
        _webSocket.SendAsync(
            new ArraySegment<byte>(buffer, 0, e.OcppResponseMessage.Length),
            WebSocketMessageType.Text,
            true,
            CancellationToken.None);
        _logger.LogInformation("FrameMessage sent: " + e.OcppResponseMessage);
    }
}