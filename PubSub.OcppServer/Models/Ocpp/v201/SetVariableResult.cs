﻿namespace PubSub.OcppServer.Models.Ocpp.v201;

public class SetVariableResult
{
    public AttributeEnum? AttributeType { get; set; }
    public SetVariableStatusEnum AttributeStatus { get; set; }
    public Component Component { get; set; }
    public Variable Variable { get; set; }
    public StatusInfo? AttributeStatusInfo { get; set; }
}