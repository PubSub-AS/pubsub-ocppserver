using PubSub.OcppServer.Models.Internal;
using PubSub.OcppServer.Services.Interfaces;

namespace PubSub.OcppServer.Services
{
    public class OcppHandlerFactory : IOcppHandlerFactory
    {
      
        private readonly IServiceProvider _serviceProvider;
        public OcppHandlerFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        public IOcppHandler CreateHandler(OcppVersionEnum ocppVersion)
        {
            if (ocppVersion == OcppVersionEnum.None)
                throw new ArgumentException("Invalid OCPP protocol version", nameof(ocppVersion));
            if (ocppVersion == OcppVersionEnum.v16) return _serviceProvider.GetRequiredService<OcppHandler16>();
            return _serviceProvider.GetRequiredService<OcppHandler201>();

        }
    }
    
}
