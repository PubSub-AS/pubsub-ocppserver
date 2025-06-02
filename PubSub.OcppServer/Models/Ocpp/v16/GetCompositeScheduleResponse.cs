using System.Text.Json.Serialization;

namespace PubSub.OcppServer.Models.Ocpp.v16
{
    public partial class GetCompositeScheduleResponse : IOcppResponse
    {
        public GetCompositeScheduleResponse(v201.ChargingSchedule chargingSchedule, long? connectorId, DateTimeOffset? scheduleStart, GetCompositeScheduleStatus status)
        {
            ChargingSchedule = chargingSchedule;
            ConnectorId = connectorId;
            ScheduleStart = scheduleStart;
            Status = status;
        }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("chargingSchedule")]
        public v201.ChargingSchedule ChargingSchedule { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("connectorId")]
        public long? ConnectorId { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("scheduleStart")]
        public DateTimeOffset? ScheduleStart { get; set; }

        [JsonPropertyName("status")] public GetCompositeScheduleStatus Status { get; set; }
    }
}