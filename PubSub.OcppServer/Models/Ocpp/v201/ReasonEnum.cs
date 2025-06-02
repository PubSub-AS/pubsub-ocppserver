﻿namespace PubSub.OcppServer.Models.Ocpp.v201;

public enum ReasonEnum
{
    DeAuthorized, EmergencyStop, EnergyLimitReached, EVDisconnected, GroundFault, ImmediateReset,
    Local, LocalOutOfCredit, MasterPass, Other, OvercurrentFault, PowerLoss, PowerQuality, Reboot,
    Remote, SOCLimitReached, StoppedByEV, TimeLimitReached, Timeout
}