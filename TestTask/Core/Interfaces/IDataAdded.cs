namespace TestTask.Core.Interfaces
{
    /// <summary>
    /// Интерфейс, представляющий методы для генерации случайных значений данных
    /// </summary>
    internal interface IDataAdded
    {
        /// <summary>
        /// Метод генерации номера телефона 
        /// </summary> 
        /// <returns> строковое представление случайного номера телефона</returns>
        public static string CreatePhoneNumber()
        {
            string result = "";
            return result;
        }
        /// <summary>
        /// Метод создания номера кредитной карты 
        /// </summary> 
        /// <returns> строковое представление случайного номера кредитной карты</returns>
        public static string CreateCreditCardNumber()
        {
            string result = "";
            return result;
        }
    }
}
