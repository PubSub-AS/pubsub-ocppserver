using PubSub.OcppServer.Models.Ocpp.v201;
using PubSub.OcppServer.Services;
using PubSub.OcppServer.Services.Interfaces;

namespace PubSub.OcppServer.OcppMessageIncomingHandlers.v201
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

            var idTokenInfo = new IdTokenInfo();

            if (request.IdToken != null && _ocppServer.IsIdTokenValid(request.IdToken))
            {
                idTokenInfo.Status = AuthorizationStatusEnum.Accepted;
                idTokenInfo.CacheExpiryDateTime = DateTime.Now.AddHours(2);
            }
            else
            {
                _logger.LogWarning($"Invalid ID Token for ChargingPoint: {_context.ChargingPointId}");
                idTokenInfo.Status = AuthorizationStatusEnum.Invalid;
            }

            return new AuthorizeResponse { IdTokenInfo = idTokenInfo };
        }
    }

}
