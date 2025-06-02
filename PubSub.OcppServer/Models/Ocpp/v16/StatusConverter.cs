namespace PubSub.OcppServer.Models.Ocpp.v16
{
    /*
    
    internal class StatusConverter : JsonConverter<Status>
    {
        public override bool CanConvert(Type t) => t == typeof(Status);

        public override Status Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var value = reader.GetString();
            switch (value)
            {
                case "Accepted":
                    return Status.Accepted;
                case "Blocked":
                    return Status.Blocked;
                case "ConcurrentTx":
                    return Status.ConcurrentTx;
                case "Expired":
                    return Status.Expired;
                case "Invalid":
                    return Status.Invalid;
            }
            throw new Exception("Cannot unmarshal type Status");
        }

        public override void Write(Utf8JsonWriter writer, Status value, JsonSerializerOptions options)
        {
            switch (value)
            {
                case Status.Accepted:
                    JsonSerializer.Serialize(writer, "Accepted", options);
                    return;
                case Status.Blocked:
                    JsonSerializer.Serialize(writer, "Blocked", options);
                    return;
                case Status.ConcurrentTx:
                    JsonSerializer.Serialize(writer, "ConcurrentTx", options);
                    return;
                case Status.Expired:
                    JsonSerializer.Serialize(writer, "Expired", options);
                    return;
                case Status.Invalid:
                    JsonSerializer.Serialize(writer, "Invalid", options);
                    return;
            }
            throw new Exception("Cannot marshal type Status");
        }

        public static readonly StatusConverter Singleton = new StatusConverter();
    }
    */
}
