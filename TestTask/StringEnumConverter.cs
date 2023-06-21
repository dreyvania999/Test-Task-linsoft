using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;

namespace TestTask
{
    internal class StringEnumConverter : JsonConverter<Gender>
    {
        public override Gender Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            string? value = reader.GetString();
            return Enum.Parse<Gender>(value, true);
        }

        public override void Write(Utf8JsonWriter writer, Gender value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString());
        }
    }
}
