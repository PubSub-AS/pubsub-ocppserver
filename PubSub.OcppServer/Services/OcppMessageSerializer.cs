using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using PubSub.OcppServer.Models.FramingProtocol;
using PubSub.OcppServer.Services.Interfaces;

namespace PubSub.OcppServer.Services
{
    public class OcppMessageSerializer : IOcppMessageSerializer

    {
        private readonly ILogger<OcppMessageSerializer> _logger;
        private readonly JsonSerializerOptions _jsonSerializerOptions;
        private const int CALL_TAG = 2;
        private const int CALL_RESULT_TAG = 3;
        private const int CALL_ERROR_TAG = 4;

        public OcppMessageSerializer(
            ILogger<OcppMessageSerializer> logger)
        {
            _logger = logger;
            _jsonSerializerOptions = new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                Converters = {
                    new JsonStringEnumConverter(),
                    new ZuluTimeConverter()
                }
            };
           
        }


        public string SerializeCall(Call call)
        {

            var result = new[]
            {
                CALL_TAG,
                call.UniqueId,
                call.Action,
                call.Payload
            };
            return JsonSerializer.Serialize(result, _jsonSerializerOptions);
        }
        public string SerializeCallResult(
            string uniqueId,
            object responsePayload
            )
        {

            var result = new[]
            {
                CALL_RESULT_TAG,
                uniqueId.Replace("\"", ""),

                responsePayload
            };
            return JsonSerializer.Serialize(result, _jsonSerializerOptions);
        }

        public string SerializeCallError(
            string uniqueId,
            string errorCode,
            string errorDescription
        )
        {
            var result = new object[]
            {
                CALL_ERROR_TAG,
                uniqueId.Replace("\"", ""),
                errorCode,
                errorDescription
            };
            return JsonSerializer.Serialize(result, _jsonSerializerOptions);
        }

        public object? DeserializeRequest(string? payload, Type? requestType)
        {
            return JsonSerializer.Deserialize(payload, requestType, _jsonSerializerOptions);

        }


    }
}
