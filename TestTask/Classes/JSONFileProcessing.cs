using TestTask.Core.Interfaces;

namespace TestTask.Classes
{
    /// <summary>
    /// Метод для работы с JSON файлами
    /// </summary>
    internal class JSONFileProcessing : IFileProcessing
    {
        private static readonly string _path = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);

        /// <summary>
        /// Метод для чтения файла
        /// </summary>
        /// <param name="FileName">Название файла</param>
        /// <returns>JSON строка представление объекта</returns>
        public static async Task<string> ReadFile(string FileName)
        {
            try
            {
                Task<string> readResultAsync = File.ReadAllTextAsync(_path + @"\" + FileName);
                string readResult = await readResultAsync;
                return readResult;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return "";
            }

        }

        /// <summary>
        /// Метод для записи файла 
        /// </summary>
        /// <param name="JSONObject">Строка для записи в формате JSON</param>
        /// <returns>Значение успешности выполнения задачи</returns>
        public static async Task<bool> WriteInFile(string JSONObject, string FileName)
        {
            try
            {
                await File.WriteAllTextAsync(_path + @"\" + FileName, JSONObject);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }

        }
    }
}
