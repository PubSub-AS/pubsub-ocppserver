namespace PubSub.OcppServer.Models.Ocpp.v201;

public enum TriggerReasonEnum
{
    Authorized, CablePluggedIn, ChargingRateChanged, ChargingStateChanged, Deauthorized,
    EnergyLimitReached, EVCommunicationLost, EVConnectTimeout, MeterValueClock
}