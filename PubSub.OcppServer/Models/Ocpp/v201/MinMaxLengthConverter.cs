using System.Text.Json;
using System.Text.Json.Serialization;

namespace PubSub.OcppServer.Models.Ocpp.v201
{

    internal class MinMaxLengthCheckConverter : JsonConverter<string>
    {
        public override bool CanConvert(System.Type t) => t == typeof(string);

        public override string Read(ref Utf8JsonReader reader, System.Type typeToConvert, JsonSerializerOptions options)
        {
            var value = reader.GetString();
            if (value.Length <= 20)
            {
                return value;
            }
            throw new Exception("Cannot unmarshal type string");
        }

        public override void Write(Utf8JsonWriter writer, string value, JsonSerializerOptions options)
        {
            if (value.Length <= 20)
            {
                JsonSerializer.Serialize(writer, value, options);
                return;
            }
            throw new Exception("Cannot marshal type string");
        }

        public static readonly MinMaxLengthCheckConverter Singleton = new MinMaxLengthCheckConverter();
    }
}
