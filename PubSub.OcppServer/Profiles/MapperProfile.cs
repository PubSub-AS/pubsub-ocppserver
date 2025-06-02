using AutoMapper;
using PubSub.OcppServer.Models.EF;
using PubSub.OcppServer.Models.Dtos;
using PubSub.OcppServer.Models.Internal;

namespace PubSub.OcppServer.Profiles
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<TransactionDto, TransactionInternal>()
            .ForMember(dest => dest.ChargingTransactionId, opt => opt.MapFrom(src => src.ChargingTransactionId))
            .ForMember(dest => dest.Connector, opt => opt.MapFrom(src => src.Connector))
            .ForMember(dest => dest.IdTagId, opt => opt.MapFrom(src => src.IdTagId))
            .ForMember(dest => dest.MeterValues, opt => opt.MapFrom(src => src.MeterValues))
            .ForMember(dest => dest.State, opt => opt.MapFrom(src => src.State)) 
            .ForMember(dest => dest.LastUpdated, opt => opt.MapFrom(src => src.LastUpdated))
            .ForMember(dest => dest.OcppVersion, opt => opt.Ignore()) // Handle OCPP version separately if needed
            .ForMember(dest => dest.ChargingTransactionIDOcppv16, opt => opt.Ignore()); // Ignore for now, handled differently
            CreateMap<ChargingTransaction, TransactionDto>()
                .ForMember(dest => dest.ChargingTransactionId, opt => opt.MapFrom(src => src.ChargingTransactionID))
                .ForMember(dest => dest.Connector, opt => opt.MapFrom(src => src.Connector))
                .ForMember(dest => dest.IdTagId, opt => opt.MapFrom(src => src.IdTagID))
                .ForMember(dest => dest.MeterValues, opt => opt.MapFrom(src => src.MeterValues))
                .ForMember(dest => dest.LastUpdated, opt => opt.MapFrom(src => src.LastUpdated))
                .ForMember(dest => dest.State, opt => opt.MapFrom(src => src.State))
                .ForMember(dest => dest.TotalKWh, opt => opt.MapFrom(src => src.TotalKWh))
                .ForMember(dest => dest.TotalSeconds, opt => opt.MapFrom(src => src.TotalSeconds))
                .ForMember(dest => dest.TotalPriceEuros, opt => opt.MapFrom(src => src.TotalPriceEuros))
                .ForMember(dest => dest.Soc, opt => opt.MapFrom(src => src.Soc))
                .ForMember(dest => dest.EffectWatts, opt => opt.MapFrom(src => src.EffectWatts));

            // Map from ChargingTransaction to TransactionInternal and vice versa
            CreateMap<ChargingTransaction, TransactionInternal>()
                .ForMember(dest => dest.ChargingTransactionId, opt => opt.MapFrom(src => src.ChargingTransactionID))
                .ForMember(dest => dest.ChargingTransactionIDOcppv16, opt => opt.MapFrom(src => src.v16Id))
                .ForMember(dest => dest.Connector, opt => opt.MapFrom(src => src.Connector))
                .ForMember(dest => dest.IdTagId, opt => opt.MapFrom(src => src.IdTagID))
                .ForMember(dest => dest.MeterValues, opt => opt.MapFrom(src => src.MeterValues))
                .ForMember(dest => dest.ChargingPointId, opt => opt.MapFrom(src => src.ChargingPointID))
                .ForMember(dest => dest.State, opt => opt.MapFrom(src => src.State))
                .ForMember(dest => dest.LastUpdated, opt => opt.MapFrom(src => src.LastUpdated))
                .ForMember(dest => dest.OcppVersion, opt => opt.Ignore()) // Ignore for now
            .ReverseMap()
                .ForMember(dest => dest.ChargingTransactionID, opt => opt.MapFrom(src => src.ChargingTransactionId))
                .ForMember(dest => dest.v16Id, opt => opt.MapFrom(src => src.ChargingTransactionIDOcppv16))
                .ForMember(dest => dest.Connector, opt => opt.MapFrom(src => src.Connector))
                .ForMember(dest => dest.IdTagID, opt => opt.MapFrom(src => src.IdTagId))
                .ForMember(dest => dest.MeterValues, opt => opt.MapFrom(src => src.MeterValues))
                .ForMember(dest => dest.ChargingPointID, opt => opt.MapFrom(src => src.ChargingPointId))
                .ForMember(dest => dest.State, opt => opt.MapFrom(src => src.State))
                .ForMember(dest => dest.LastUpdated, opt => opt.MapFrom(src => src.LastUpdated.ToUniversalTime()));

            // Map between InternalMeterValue and MeterValue (string <--> double)
            CreateMap<MeterValueInternal, MeterValue>();
                //.ForMember(dest => dest.ValueRaw, opt => opt.MapFrom(src => src.ValueRaw));
            CreateMap<MeterValue, MeterValueInternal>()
                .ForMember(dest => dest.MeterValueID, opt => opt.MapFrom(src => src.MeterValueID))
                .ForMember(dest => dest.ChargingTransactionID, opt => opt.MapFrom(src => src.ChargingTransactionID))
                .ForMember(dest => dest.Timestamp, opt => opt.MapFrom(src => src.Timestamp))
                .ForMember(dest => dest.ValueRaw, opt => opt.MapFrom(src => src.ValueRaw))
                .ForMember(dest => dest.ValueSignedData, opt => opt.MapFrom(src => src.ValueSignedData))
                .ForMember(dest => dest.Unit, opt => opt.MapFrom(src => src.Unit))
                .ForMember(dest => dest.Context, opt => opt.MapFrom(src => src.Context))
                .ForMember(dest => dest.Location, opt => opt.MapFrom(src => src.Location))
                .ForMember(dest => dest.Measurand, opt => opt.MapFrom(src => src.Measurand))
                .ForMember(dest => dest.Phase, opt => opt.MapFrom(src => src.Phase));
            CreateMap<MeterValue, MeterValueDto>();

            CreateMap<ConnectorInternal, Connector>()
                .ForMember(dest => dest.ChargingPointId, opt => opt.MapFrom(src => src.ChargingPointId))
                .ForMember(dest => dest.ConnectorName, opt => opt.MapFrom(src => src.ConnectorId))
                .ForMember(dest => dest.State, opt => opt.MapFrom(src => src.State))
                .ReverseMap()
                .ForMember(dest => dest.ConnectorId, opt => opt.MapFrom(src => src.ConnectorName))
                .ForMember(dest => dest.ChargingPointId, opt => opt.MapFrom(src => src.ChargingPointId))
                .ForMember(dest => dest.State, opt => opt.MapFrom(src => src.State));
            CreateMap<ConnectorInternal, ConnectorDto>()
                .ForMember(dest => dest.ConnectorId, opt => opt.MapFrom(src => src.ConnectorId))
                .ForMember(dest => dest.State, opt => opt.MapFrom(src => src.State))
                .ReverseMap()
                .ForMember(dest => dest.ConnectorId, opt => opt.MapFrom(src => src.ConnectorId))
                .ForMember(dest => dest.State, opt => opt.MapFrom(src => src.State));
            CreateMap<ChargingPoint, ChargingPointDto>()
                .ForMember(dest => dest.ChargingPointID, opt => opt.MapFrom(src => src.ChargingPointID))
                .ForMember(dest => dest.Facility, opt => opt.MapFrom(src => src.Facility.FacilityName))
                .ForMember(dest => dest.Model, opt => opt.MapFrom(src => src.ChargePointModel))
                .ForMember(dest => dest.State, opt => opt.MapFrom(src => src.State));
            CreateMap<Facility, FacilityDto>()
                .ForMember(dest => dest.ChargingPoints, opt => opt.MapFrom(src => src.ChargingPoints))
                .ForMember(dest => dest.EnergyZoneName, opt => opt.MapFrom(src => src.EnergyZoneName))
                .ForMember(dest => dest.FacilityID, opt => opt.MapFrom(src => src.FacilityID))
                .ForMember(dest => dest.FacilityName, opt => opt.MapFrom(src => src.FacilityName))
                .ForMember(dest => dest.FacilityOwner, opt => opt.MapFrom(src => src.FacilityOwner.FacilityOwnerName))
                .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.UserId));

        }
    }
}
