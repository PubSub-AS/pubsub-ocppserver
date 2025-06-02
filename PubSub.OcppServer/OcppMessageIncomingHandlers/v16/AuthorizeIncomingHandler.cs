using PubSub.OcppServer.Models.Ocpp.v16;
using PubSub.OcppServer.Services;
using PubSub.OcppServer.Services.Interfaces;

namespace PubSub.OcppServer.OcppMessageIncomingHandlers.v16
{
    public class AuthorizeIncomingHandler : IOcppMessageIncomingHandler<AuthorizeRequest, AuthorizeResponse>
    {
        private readonly IOcppServer _ocppServer;
        private readonly ILogger<AuthorizeIncomingHandler> _logger;
        private readonly OcppHandlerContext _context;

        public AuthorizeIncomingHandler(IOcppServer ocppServer, ILogger<AuthorizeIncomingHandler> logger, OcppHandlerContext context)
        {
            _ocppServer = ocppServer;
            _logger = logger;
            _context = context;
        }

        public AuthorizeResponse Handle(AuthorizeRequest request)
        {
            _logger.LogInformation($"Handling Authorize Request for ChargingPoint: {_context.ChargingPointId}");

            var idTagInfo = new IdTagInfo();

            if (request.IdTag != null && _ocppServer.IsIdTagValid(request.IdTag))
            {
                idTagInfo.Status = AuthorizationStatus.Accepted;
                idTagInfo.ExpiryDate = DateTimeOffset.Now.AddHours(2);
            }
            else
            {
                _logger.LogWarning($"Invalid ID Tag for ChargingPoint: {_context.ChargingPointId}");
                idTagInfo.Status = AuthorizationStatus.Invalid;
            }

            return new AuthorizeResponse { IdTagInfo = idTagInfo };
        }
    }

}
