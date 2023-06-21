using System.Text.Json;
using TestTask.Classes;
using TestTask.Core.DataModel;

namespace TestTask
{
    internal class Program
    {

        private const string FileName = "Persons.json";
        private const int PersonCount = 10000;

        private static async Task Main()
        {
            List<Person>? persons = await DataGenerator.GeneratePersonsAsync(PersonCount);
            JsonSerializerOptions options = new();
            string json = JsonSerializer.Serialize(persons, options);
            _ = await JSONFileProcessing.WriteInFile(json, FileName);
            DisplayedInformation.DisplayCollection(persons);

            Console.WriteLine("Нажмите на любую кнопку для продолжения работу");
            _ = Console.ReadKey();
            persons.Clear();
            Console.Clear();

            json = JSONFileProcessing.ReadFile(FileName).Result;
            persons = JsonSerializer.Deserialize<List<Person>>(json, options);
            DisplayedInformation.DisplayResult(persons);
        }
    }
}