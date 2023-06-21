namespace TestTask.Classes
{
    /// <summary> 
    /// Статический класс, который расширяет тип DateTime 
    /// </summary> 
    internal static class DateTimeExtensions
    {
        // Константа, которая представляет начало эпохи Unix в формате UTC
        private static readonly DateTime UnixEpoch = new(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        /// <summary>
        /// Метод расширения, который преобразует дату в миллисекунды с начала эпохи Unix
        /// </summary>
        /// <param name="dateTime">Дата, которую нужно преобразовать</param>
        /// <returns>Количество миллисекунд с начала эпохи Unix</returns>
        public static long ToUnixTimeMilliseconds(this DateTime dateTime)
        {
            return (long)(dateTime.ToUniversalTime() - UnixEpoch).TotalMilliseconds;
        }
        /// <summary>
        /// Метод расширения, который преобразует миллисекунды с начала эпохи Unix в дату
        /// </summary>
        /// <param name="milliseconds">Количество миллисекунд с начала эпохи Unix</param>
        /// <returns>Дата, соответствующая заданному количеству миллисекунд</returns>
        public static DateTime FromUnixTimeMilliseconds(this long milliseconds)
        {
            return UnixEpoch.AddMilliseconds(milliseconds);
        }
    }
}
