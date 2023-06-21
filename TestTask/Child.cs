using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TestTask
{
    internal class Child
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("firstName")]
        public string? FirstName { get; set; }

        [JsonPropertyName("lastName")]
        public string? LastName { get; set; }
        [JsonConverter(typeof(PosixDateTimeConverter))]
        [JsonPropertyName("birthDate")]
        public long BirthDate { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        [JsonPropertyName("gender")]
        public Gender Gender { get; set; }
        [JsonIgnore]
        public int Age => (int)((DateTime.Now - BirthDate.FromUnixTimeMilliseconds()).TotalDays / 365.25);
    }
}
