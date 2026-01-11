using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Library.Shared.Extensions
{
    public class StringOrArrayConverter : JsonConverter<List<string>>
    {
        public override List<string> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.String)
                return new List<string> { reader.GetString()! };

            if (reader.TokenType == JsonTokenType.StartArray)
            {
                var list = new List<string>();
                while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
                    list.Add(reader.GetString()!);

                return list;
            }

            throw new JsonException("invalid format");
        }

        public override void Write(Utf8JsonWriter writer, List<string> value, JsonSerializerOptions options)
            => JsonSerializer.Serialize(writer, value, options);
    }
}

