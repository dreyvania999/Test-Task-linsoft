using System.Text.Json;
using System.Text.Json.Serialization;

namespace TestTask
{
    internal class Program
    {

        private const string FileName = "Persons.json";
        private const int PersonCount = 10000;
        private const int MinAge = 18;
        private const int MaxAge = 65;
        private const int MinChildren = 0;
        private const int MaxChildren = 4;
        private const int MinCreditCards = 1;
        private const int MaxCreditCards = 4;
        private const double MinSalary = 20000;
        private const double MaxSalary = 120000;

        private static async Task Main(string[] args)
        {
            List<Person>? persons = await GeneratePersonsAsync(PersonCount);
            JsonSerializerOptions options = new();
            string json = JsonSerializer.Serialize(persons, options);
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string filePath = Path.Combine(desktopPath, FileName);
            await File.WriteAllTextAsync(filePath, json);
            persons.Clear();
            json = await File.ReadAllTextAsync(filePath);
            persons = JsonSerializer.Deserialize<List<Person>>(json, options);
            Console.WriteLine($"Persons count: {persons.Count}");
            Console.WriteLine($"Persons credit card count: {persons.Sum(p => p.CreditCardNumbers.Length)}");
            Console.WriteLine($"The average value of child age: {persons.SelectMany(p => p.Children).Average(c => c.Age):F2}");
        }
        private static async Task<List<Person>> GeneratePersonsAsync(int count)
        {
            Random random = new();
            string[] firstNames = new[] { "Александр", "Анна", "Дмитрий", "Елена", "Иван", "Мария", "Олег", "Светлана" };
            string[] lastNames = new[] { "Иванов", "Петров", "Сидоров", "Смирнов", "Кузнецов", "Попов", "Новиков", "Соколов" };
            Gender[] genders = Enum.GetValues<Gender>();
            string[] phones = new[] { "+7(900)111-11-11", "+7(900)222-22-22", "+7(900)333-33-33", "+7(900)444-44-44" };

            List<Person> persons = new();
            _ = await Task.Run(() => Parallel.ForEach(Enumerable.Range(0, count), i =>
            {
                string firstName = firstNames[random.Next(firstNames.Length)];
                string lastName = lastNames[random.Next(lastNames.Length)];
                Gender gender = genders[random.Next(genders.Length)];
                int age = random.Next(MinAge, MaxAge + 1);
                long birthDate = DateTime.Now.AddYears(-age).ToUnixTimeMilliseconds();
                double salary = (random.NextDouble() * (MaxSalary - MinSalary)) + MinSalary;
                bool isMarried = random.NextDouble() < 0.5;
                string[] creditCardNumbers = Enumerable.Range(0, random.Next(MinCreditCards, MaxCreditCards + 1)).Select(_ => Guid.NewGuid().ToString()).ToArray();
                Guid transportId = Guid.NewGuid();
                int sequenceId = i + 1;
                int phonesCount = random.Next(1, phones.Length + 1);
                string[] phonesList = phones.OrderBy(_ => random.Next()).Take(phonesCount).ToArray();

                int childrenCount = random.Next(MinChildren, MaxChildren + 1);
                List<Child> children = new();

                for (int j = 0; j < childrenCount; j++)
                {
                    string childFirstName = firstNames[random.Next(firstNames.Length)];
                    string childLastName = lastName;
                    Gender childGender = genders[random.Next(genders.Length)];
                    int childAge = random.Next(0, age - 18);
                    long childBirthDate = DateTime.Now.AddYears(-childAge).ToUnixTimeMilliseconds();

                    Child child = new()
                    {
                        Id = j + 1,
                        FirstName = childFirstName,
                        LastName = childLastName,
                        BirthDate = childBirthDate,
                        Gender = childGender
                    };

                    children.Add(child);
                }

                Person person = new()
                {
                    Id = i + 1,
                    TransportId = transportId,
                    FirstName = firstName,
                    LastName = lastName,
                    SequenceId = sequenceId,
                    CreditCardNumbers = creditCardNumbers,
                    Age = age,
                    Phones = phonesList,
                    BirthDate = birthDate,
                    Salary = salary,
                    IsMarried = isMarried,
                    Gender = gender,
                    Children = children.ToArray()
                };
                lock (persons)
                {
                    persons.Add(person);
                }
            }));

            return persons;
        }
    }
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

    internal enum Gender
    {
        Male,
        Female
    }
    internal class PosixDateTimeConverter : JsonConverter<long>
    {
        public override long Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return reader.GetInt64();
        }

        public override void Write(Utf8JsonWriter writer, long value, JsonSerializerOptions options)
        {
            writer.WriteNumberValue(value);
        }
    }

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
    internal static class DateTimeExtensions
    {
        private static readonly DateTime UnixEpoch = new(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        public static long ToUnixTimeMilliseconds(this DateTime dateTime)
        {
            return (long)(dateTime.ToUniversalTime() - UnixEpoch).TotalMilliseconds;
        }

        public static DateTime FromUnixTimeMilliseconds(this long milliseconds)
        {
            return UnixEpoch.AddMilliseconds(milliseconds);
        }
    }

}