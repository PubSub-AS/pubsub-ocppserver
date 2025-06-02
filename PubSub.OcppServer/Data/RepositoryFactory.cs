using PubSub.OcppServer.Data.Interfaces;

namespace PubSub.OcppServer.Data
{
    public class RepositoryFactory : IRepositoryFactory
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public RepositoryFactory(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public IChargingTransactionRepository CreateChargingTransactionRepository(ChargingContext context, IHttpContextAccessor httpContextAccessor) =>
            new ChargingTransactionRepository(context, _httpContextAccessor);

        public IChargingPointRepository CreateChargingPointRepository(ChargingContext context, IHttpContextAccessor httpContextAccessor) =>
            new ChargingPointRepository(context, _httpContextAccessor);

        public IChargingPointProviderRepository CreateChargingPointProviderRepository(ChargingContext context) =>
            new ChargingPointProviderRepository(context);
        public IChargingSchedulePeriodRepository CreateChargingSchedulePeriodRepository(ChargingContext context) =>
            new ChargingSchedulePeriodRepository(context);
        public IEvseRepository CreateEvseRepository(ChargingContext context) => 
            new EvseRepository(context);
        public IFacilityRepository CreateFacilityRepository(ChargingContext context) =>
            new FacilityRepository(context, _httpContextAccessor);

        public IFacilityOwnerRepository CreateFacilityOwnerRepository(ChargingContext context) =>
            new FacilityOwnerRepository(context);

        public IIdTagRepository CreateIdTagRepository(ChargingContext context) =>
            new IdTagRepository(context);

        public IUserRepository CreateUserRepository(ChargingContext context) =>
            new UserRepository(context);

        public IApiUserRepository CreateApiUserRepository(ChargingContext context) =>
            new ApiUserRepository(context);

        public IChargePointUserRepository CreateChargePointUserRepository(ChargingContext context) =>
            new ChargePointUserRepository(context);

        public IMeterValueRepository CreateMeterValueRepository(ChargingContext context) =>
            new MeterValueRepository(context);

        public IConnectorRepository CreateConnectorRepository(ChargingContext context) =>
            new ConnectorRepository(context);

        public IReservationRepository CreateReservationRepository(ChargingContext context) =>
            new ReservationRepository(context);

        public IVehicleRepository CreateVehicleRepository(ChargingContext context) =>
            new VehicleRepository(context);
    }

}
