namespace PubSub.OcppServer.Data.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IChargingTransactionRepository ChargingTransactions { get; }
        IChargingPointRepository ChargingPoints { get; }
        IChargingPointProviderRepository ChargingPointProviders { get; }
        IChargingSchedulePeriodRepository ChargingSchedulePeriods { get; }
        IConnectorRepository Connectors { get; }
        IEvseRepository Evses { get; }
        IFacilityRepository Facilities { get; }
        IFacilityOwnerRepository FacilityOwners { get; }
        IIdTagRepository IdTags { get; }
        IMeterValueRepository MeterValues { get; }
        IReservationRepository Reservations { get; }
        IUserRepository Users { get; }
        IApiUserRepository ApiUsers { get; }
        IChargePointUserRepository ChargePointUsers { get; }
        IVehicleRepository Vehicles { get; }
        
       
        int Complete();
    }
}
