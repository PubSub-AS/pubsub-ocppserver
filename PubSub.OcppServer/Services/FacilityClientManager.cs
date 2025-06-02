using AutoMapper;
using PubSub.OcppServer.Controllers;
using PubSub.OcppServer.Models.Dtos;
using PubSub.OcppServer.Models.EF;
using PubSub.OcppServer.Models.EventArguments;
using PubSub.OcppServer.Services.Interfaces;
using System.Threading;

namespace PubSub.OcppServer.Services
{
    public class FacilityClientManager : IFacilityClientManager
    {
        private readonly ILogger<FacilityClientManager> _logger;
        private readonly Dictionary<int, List<Action<TransactionDto>>> _handlers = new();
        private readonly object _lock = new(); // for thread-safety
        private readonly IMapper _mapper;
        public FacilityClientManager(
            ILogger<FacilityClientManager> logger,
            IMapper mapper)
        {
            _logger = logger;
            _mapper = mapper;
        }

        public void RegisterHandler(int facilityId, Action<TransactionDto> handler)
        {
            lock (_lock)
            {
                if (!_handlers.TryGetValue(facilityId, out var list))
                {
                    list = new List<Action<TransactionDto>>();
                    _handlers[facilityId] = list;
                }

                list.Add(handler);
            }
        }

        public void UnregisterHandler(int facilityId, Action<TransactionDto> handler)
        {
            lock (_lock)
            {
                if (_handlers.TryGetValue(facilityId, out var list))
                {
                    list.Remove(handler);
                    if (list.Count == 0)
                        _handlers.Remove(facilityId);
                }
            }
        }

        public void RouteMessage(int facilityId, ChargingTransaction chargingTransaction)
        {
            var message = _mapper.Map<TransactionDto>(chargingTransaction);
            message.MeterValues.Clear(); // we don't need the MeterValues in the depot overview
            List<Action<TransactionDto>> handlersToInvoke;
            lock (_lock)
            {
                if (!_handlers.TryGetValue(facilityId, out var list)) return;
                handlersToInvoke = list.ToList(); // Clone to avoid issues during iteration
            }

            foreach (var handler in handlersToInvoke)
            {
                try
                {
                    handler(message);
                }
                catch (Exception ex)
                {
                    
                }
            }
        }
    }
}
