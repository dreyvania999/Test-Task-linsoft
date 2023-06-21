using System.Text.Json.Serialization;
using TestTask.Classes;

namespace TestTask.Core.DataModel
{
    internal class Person
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("transportId")]
        public Guid TransportId { get; set; }

        [JsonPropertyName("firstName")]
        public string? FirstName { get; set; }

        [JsonPropertyName("lastName")]
        public string? LastName { get; set; }

        [JsonPropertyName("sequenceId")]
        public int SequenceId { get; set; }

        [JsonPropertyName("creditCardNumbers")]
        public string[]? CreditCardNumbers { get; set; }
        [JsonIgnore]
        public int Age { get; set; }

        [JsonPropertyName("phones")]
        public string[]? Phones { get; set; }
        [JsonConverter(typeof(PosixDateTimeConverter))]
        [JsonPropertyName("birthDate")]
        public long BirthDate { get; set; }
        [JsonNumberHandling(JsonNumberHandling.AllowReadingFromString | JsonNumberHandling.WriteAsString)]
        [JsonPropertyName("salary")]
        public double Salary { get; set; }

        [JsonPropertyName("isMarried")]
        public bool IsMarried { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        [JsonPropertyName("gender")]
        public Gender Gender { get; set; }
        [JsonInclude]
        [JsonPropertyName("children")]
        public Child[]? Children { get; set; }
    }
}
