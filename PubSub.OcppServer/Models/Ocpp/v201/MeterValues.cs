using System.Reflection.Metadata;
using System.Text.Json.Serialization;
using Microsoft.VisualBasic.CompilerServices;

namespace PubSub.OcppServer.Models.Ocpp.v201
{
    public class MeterValues
    {
        public MeterValues(long connectorId, MeterValue[] meterValue, long? transactionId)
        {
            ConnectorId = connectorId;
            MeterValue = meterValue;
            TransactionId = transactionId;
        }

        [JsonPropertyName("connectorId")] public long ConnectorId { get; set; }

        [JsonPropertyName("meterValue")] public MeterValue[] MeterValue { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("transactionId")]
        public long? TransactionId { get; set; }
    }


}