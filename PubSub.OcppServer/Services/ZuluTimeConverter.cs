namespace PubSub.OcppServer.Services
{
    using System;
    using System.Text.Json;
    using System.Text.Json.Serialization;

    public class ZuluTimeConverter : JsonConverter<DateTimeOffset>
    {
        public override DateTimeOffset Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            // Parse the DateTimeOffset from the incoming JSON
            return DateTimeOffset.Parse(reader.GetString()!);
        }

        public override void Write(Utf8JsonWriter writer, DateTimeOffset value, JsonSerializerOptions options)
        {
            // Format DateTimeOffset as UTC with "Z" (Zulu time) suffix
            writer.WriteStringValue(value.UtcDateTime.ToString("yyyy-MM-ddTHH:mm:ss.fffffffZ"));
        }
    }
}
