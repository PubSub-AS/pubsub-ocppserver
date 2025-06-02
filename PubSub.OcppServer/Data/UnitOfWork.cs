using System.Diagnostics.Eventing.Reader;
using PubSub.OcppServer.Data.Interfaces;
using PubSub.OcppServer.Models.Ocpp.v201;

namespace PubSub.OcppServer.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ChargingContext _context;
        private readonly IRepositoryFactory _repositoryFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UnitOfWork(ChargingContext context, 
            IRepositoryFactory repositoryFactory,
            IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _repositoryFactory = repositoryFactory;
            _httpContextAccessor = httpContextAccessor;

            ChargingTransactions = _repositoryFactory.CreateChargingTransactionRepository(_context, _httpContextAccessor);
            ChargingPoints = _repositoryFactory.CreateChargingPointRepository(_context, _httpContextAccessor);
            ChargingPointProviders = _repositoryFactory.CreateChargingPointProviderRepository(_context);
            ChargingSchedulePeriods = _repositoryFactory.CreateChargingSchedulePeriodRepository(_context);
            Evses = _repositoryFactory.CreateEvseRepository(_context);
            Facilities = _repositoryFactory.CreateFacilityRepository(_context);
            FacilityOwners = _repositoryFactory.CreateFacilityOwnerRepository(_context);
            IdTags = _repositoryFactory.CreateIdTagRepository(_context);
            Users = _repositoryFactory.CreateUserRepository(_context);
            ApiUsers = _repositoryFactory.CreateApiUserRepository(_context);
            ChargePointUsers = _repositoryFactory.CreateChargePointUserRepository(_context);
            MeterValues = _repositoryFactory.CreateMeterValueRepository(_context);
            Connectors = _repositoryFactory.CreateConnectorRepository(_context);
            Reservations = _repositoryFactory.CreateReservationRepository(_context);
            Vehicles = _repositoryFactory.CreateVehicleRepository(_context);
        }

        public IChargingTransactionRepository ChargingTransactions { get; }
        public IChargingPointRepository ChargingPoints { get; }
        public IChargingPointProviderRepository ChargingPointProviders { get; }
        public IChargingSchedulePeriodRepository ChargingSchedulePeriods { get; }
        public IEvseRepository Evses { get; }
        public IFacilityRepository Facilities { get; }
        public IFacilityOwnerRepository FacilityOwners { get; }
        public IIdTagRepository IdTags { get; }
        public IUserRepository Users { get; }
        public IApiUserRepository ApiUsers { get; }
        public IChargePointUserRepository ChargePointUsers { get; }
        public IMeterValueRepository MeterValues { get; }
        public IConnectorRepository Connectors { get; }
        public IReservationRepository Reservations { get; }
        public IVehicleRepository Vehicles { get; }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }

}
