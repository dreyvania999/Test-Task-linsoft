using TestTask.Core.DataModel;
using TestTask.Core.Interfaces;

namespace TestTask.Classes
{
    internal class DataGenerator : IDataAdded
    {
        private const int MinAge = 18;
        private const int MaxAge = 65;
        private const int MinChildren = 0;
        private const int MaxChildren = 4;
        private const int MinCreditCards = 1;
        private const int MaxCreditCards = 4;
        private const int MinPhones = 1;
        private const int MaxPhones = 4;
        private const double MinSalary = 20000;
        private const double MaxSalary = 120000;
        private static readonly Random random = new();
        private static readonly string[] firstNames = new[] { "Александр", "Анна", "Дмитрий", "Елена", "Иван", "Мария", "Олег", "Светлана" };
        private static readonly string[] lastNames = new[] { "Иванов", "Петров", "Сидоров", "Смирнов", "Кузнецов", "Попов", "Новиков", "Соколов" };
        private static readonly Gender[] genders = Enum.GetValues<Gender>();

        /// <summary>
        /// Метод создания номера кредитной карты
        /// </summary>
        /// <returns> строковое представление случайного номера кредитной карты</returns>
        public static string CreateCreditCardNumber()
        {
            try
            {
                int num = random.Next(1000, 10000);
                int num1 = random.Next(1000, 10000);
                int num2 = random.Next(1000, 10000);
                int num3 = random.Next(1000, 10000);
                string result = $"{num} {num1} {num2} {num3}";
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return string.Empty;

            }

        }



        /// <summary>
        /// Метод для генерации номеров телефона
        /// </summary>
        /// <returns> строка рандомного номера телефона</returns>
        public static string CreatePhoneNumber()
        {
            try
            {
                int num = random.Next(000, 999);
                int num1 = random.Next(000, 999);
                int num2 = random.Next(00, 99);
                int num3 = random.Next(00, 99);
                string result = "+7 " + num.ToString() + " " + num1.ToString() + " " + num2.ToString() + " " + num3.ToString();
                return result;
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.ToString());
                return "";
            }

        }

        public static async Task<List<Person>> GeneratePersonsAsync(int count)
        {
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
                string[] creditCardNumbers = new string[random.Next(MinCreditCards, MaxCreditCards)];
                Guid transportId = Guid.NewGuid();
                int sequenceId = i + 1;
                for (int j = 0; j < creditCardNumbers.Length; j++)
                {
                    creditCardNumbers[j] = CreateCreditCardNumber();
                }
                int childrenCount = random.Next(MinChildren, MaxChildren + 1);
                List<Child> children = new();
                string[] personPhones = new string[random.Next(MinPhones, MaxPhones)];
                for (int j = 0; j < personPhones.Length; j++)
                {
                    personPhones[j] = CreatePhoneNumber();
                }
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
                    Phones = personPhones,
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
