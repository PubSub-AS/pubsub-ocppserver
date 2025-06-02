namespace PubSub.OcppServer.Models.Ocpp.v16
{
    /*
     internal class DecodeArrayConverter : JsonConverter<string[]>
     
    {
        
        public override bool CanConvert(Type t) => t == typeof(string[]);

        public override string[] Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            reader.Read();
            var value = new List<string>();
            while (reader.TokenType != JsonToken.EndArray)
            {
                var converter = MinMaxLengthCheckConverter.Singleton;
                var arrayItem = (string)converter.ReadJson(reader, typeof(string), null, serializer);
                value.Add(arrayItem);
                reader.Read();
            }
            return value.ToArray();
        }

        public override void Write(Utf8JsonWriter writer, string[] value, JsonSerializerOptions options)
        {
            writer.WriteStartArray();
            foreach (var arrayItem in value)
            {
                var converter = MinMaxLengthCheckConverter.Singleton;
                converter.WriteJson(writer, arrayItem, serializer);
            }
            writer.WriteEndArray();
            return;
        }

        public static readonly DecodeArrayConverter Singleton = new DecodeArrayConverter();
        
    }
    */
}
