using TestTask.Core.DataModel;
using TestTask.Core.Interfaces;

namespace TestTask.Classes
{
    internal class DisplayedInformation : IDataDisplay
    {
        /// <summary>
        /// Отображение коллекции в памяти 
        /// </summary>
        /// <param name="personList">Отображаемая коллекция</param>
        public static void DisplayCollection(IEnumerable<Person> personList)
        {
            try
            {
                foreach (Person person in personList)
                {
                    Console.WriteLine($"{person.Id} - id , {person.CreditCardNumbers.Length} - количество кредитных карт, {person.FirstName} {person.LastName} , {person.Phones}  - количество телефонных номеров ");
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }

        }
        /// <summary>
        /// Отображение коллекции в памяти 
        /// </summary>
        /// <param name="personList">Отображаемая коллекция</param>
        public static void DisplayResult(IEnumerable<Person> persons)
        {
            try
            {
                Console.WriteLine($"Количество человек: {persons.Count()}");
                Console.WriteLine($"Количество кредитных карт: {persons.Sum(p => p.CreditCardNumbers.Length)}");
                Console.WriteLine($"Средний возраст детей: {persons.SelectMany(p => p.Children).Average(c => c.Age):F2}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
    }
}
