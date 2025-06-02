namespace PubSub.OcppServer.Data.Interfaces
{
    public interface IRepositoryFactory
    {
        IChargingTransactionRepository CreateChargingTransactionRepository(ChargingContext context, IHttpContextAccessor httpContextAccessor);
        IChargingPointRepository CreateChargingPointRepository(ChargingContext context, IHttpContextAccessor httpContextAccessor);
        IChargingPointProviderRepository CreateChargingPointProviderRepository(ChargingContext context);
        IChargingSchedulePeriodRepository CreateChargingSchedulePeriodRepository(ChargingContext context);
        IEvseRepository CreateEvseRepository(ChargingContext context);
        IFacilityRepository CreateFacilityRepository(ChargingContext context);
        IFacilityOwnerRepository CreateFacilityOwnerRepository(ChargingContext context);
        IIdTagRepository CreateIdTagRepository(ChargingContext context);
        IUserRepository CreateUserRepository(ChargingContext context);
        IApiUserRepository CreateApiUserRepository(ChargingContext context);
        IChargePointUserRepository CreateChargePointUserRepository(ChargingContext context);
        IMeterValueRepository CreateMeterValueRepository(ChargingContext context);
        IConnectorRepository CreateConnectorRepository(ChargingContext context);
        IReservationRepository CreateReservationRepository(ChargingContext context);
        IVehicleRepository CreateVehicleRepository(ChargingContext context);
    }

}
