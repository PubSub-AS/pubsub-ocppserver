using System.Text.Json.Serialization;

namespace PubSub.OcppServer.Models.Ocpp.v16
{
    public partial class MeterValuesRequest : IOcppRequest
    {
        public MeterValuesRequest(int connectorId, MeterValue[] meterValue, int? transactionId)
        {
            ConnectorId = connectorId;
            MeterValue = meterValue;
            TransactionId = transactionId;
        }

        [JsonPropertyName("connectorId")] public int ConnectorId { get; set; }

        [JsonPropertyName("meterValue")] public MeterValue[] MeterValue { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("transactionId")]
        public int? TransactionId { get; set; }
    }
}