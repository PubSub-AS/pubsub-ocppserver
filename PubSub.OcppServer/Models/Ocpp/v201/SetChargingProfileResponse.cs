using System.Reflection.Metadata.Ecma335;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Razor.Language.Intermediate;

namespace PubSub.OcppServer.Models.Ocpp.v201
{
    public class SetChargingProfileResponse : IOcppResponse
    {
        public ChargingProfileStatusEnum Status { get; set; }
        public StatusInfo? StatusInfo { get; set; }
    }
}



