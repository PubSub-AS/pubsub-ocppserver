using PubSub.OcppServer.Models.FramingProtocol;
using PubSub.OcppServer.OcppMessageIncomingHandlers.v16;
using PubSub.OcppServer.Services.Interfaces;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace PubSub.OcppServer.Services
{
    public class OcppMessageDispatcher : IOcppMessageDispatcher
    {
        private readonly ILogger<OcppMessageDispatcher> _logger;
        private readonly IOcppRequestManager _ocppRequestManager;
        private readonly IOcppClientManager _ocppClientManager;
        private readonly IOcppHandlerFactory _ocppHandlerFactory;

        public OcppMessageDispatcher(
            ILogger<OcppMessageDispatcher> logger,
            IOcppRequestManager ocppRequestManager,
            IOcppHandlerFactory ocppHandlerFactory,
            IOcppClientManager ocppClientManager)
        {
            _logger = logger;
            _ocppRequestManager = ocppRequestManager;
            _ocppHandlerFactory = ocppHandlerFactory;
            _ocppClientManager = ocppClientManager;
        }
        public async Task HandleIncomingOcppMessage(string rawMessage, string chargingPoint)
        {
            _logger.LogInformation("Received raw message: " + rawMessage);

            // Handle response to a previous request
            if (!rawMessage.StartsWith("[2,")) // Check if the message is a response (not a new request)
            {
                bool removed = false;
                if (rawMessage.StartsWith("[3,"))
                {
                    // Handle success response
                    var callResult = new CallResult(rawMessage);
                    removed = _ocppRequestManager
                        .TryHandleResponse(callResult);
                }
                else if (rawMessage.StartsWith("[4,"))
                {
                    // Handle error response
                    var callError = new CallError(rawMessage);
                    removed = _ocppRequestManager
                        .TryHandleError(callError);

                }
                if (!removed) _logger
                    .LogDebug("Could not find and remove message from TCS");
                // the message was outgoing and has been parsed, so we can return
                return;
            }

            // Now we know that the message is incoming
            var ocppVersion = _ocppClientManager.GetOcppVersion(chargingPoint);
            var ocppHandler = _ocppHandlerFactory.CreateHandler(ocppVersion);
            ocppHandler.HandleIncomingRequest(rawMessage);
        }


    }
}
