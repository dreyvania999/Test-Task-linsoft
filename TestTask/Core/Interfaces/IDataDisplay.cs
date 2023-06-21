
using TestTask.Core.DataModel;

namespace TestTask.Core.Interfaces
{
    /// <summary>
    /// Интерфейс, представляющий метод для отображения информации о людях
    /// </summary>
    internal interface IDataDisplay
    {
        /// <summary>
        /// Отображение коллекции
        /// </summary>
        /// <param name="personList">Отображаемая коллекция людей</param>
        public static void DisplayResult(IEnumerable<Person> personList)
        {

        }
        /// <summary>
        /// Отображение коллекции в памяти 
        /// </summary>
        /// <param name="personList">Отображаемая коллекция</param>
        public static void DisplayCollection(IEnumerable<Person> personList)
        {

        }
    }
}
