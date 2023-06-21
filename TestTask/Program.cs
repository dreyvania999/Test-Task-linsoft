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
    

    

    

    
    

}