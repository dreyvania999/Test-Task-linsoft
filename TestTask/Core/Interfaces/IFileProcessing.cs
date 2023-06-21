namespace TestTask.Core.Interfaces
{
    /// <summary>
    /// Предоставляет методы для работы с файлом JSON
    /// </summary>
    internal interface IFileProcessing
    {
        /// <summary>
        /// Метод записи в файл 
        /// </summary> 
        /// <param name=“Объект”>записываемые данные</param> 
        /// <returns>Булева переменная, сигнализирующая о удаче в завершении</returns> 
        public static bool WriteInFile(string obj)
        {
            return true;
        }

        /// <summary>
        /// Метод чтения из файла
        /// </summary>
        /// <returns>Данные из файла</returns>
        public static string ReadFile()
        {
            string readResult = "";
            return readResult;
        }
    }
}
