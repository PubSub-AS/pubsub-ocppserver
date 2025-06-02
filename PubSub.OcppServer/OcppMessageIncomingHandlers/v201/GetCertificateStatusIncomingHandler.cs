using PubSub.OcppServer.Services.Interfaces;
using PubSub.OcppServer.Services;
using PubSub.OcppServer.Models.Ocpp.v201;

namespace PubSub.OcppServer.OcppMessageIncomingHandlers.v201
{
    public class GetCertificateStatusIncomingHandler : IOcppMessageIncomingHandler<GetCertificateStatusRequest, GetCertificateStatusResponse>
    {
        private readonly IOcppServer _ocppServer;
        private readonly ILogger<GetCertificateStatusIncomingHandler> _logger;
        private readonly OcppHandlerContext _context;  // Inject the context

        public GetCertificateStatusIncomingHandler(
            IOcppServer ocppServer,
            ILogger<GetCertificateStatusIncomingHandler> logger,
            OcppHandlerContext context)
        {
            _ocppServer = ocppServer;
            _logger = logger;
            _context = context;
        }

        public GetCertificateStatusResponse Handle(GetCertificateStatusRequest request)
        {
            _logger.LogInformation($"Received GetCertificateStatus request for ChargingPoint: {_context.ChargingPointId}");
            _logger.LogInformation("This has to do with OSCP and is not yet supported");
     

            return new GetCertificateStatusResponse()
            {
                Status = GetCertificateStatusEnum.Accepted,
                
            };

        }
    }
}
