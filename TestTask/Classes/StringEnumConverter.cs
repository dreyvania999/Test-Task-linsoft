using System.Text.Json;
using System.Text.Json.Serialization;
using TestTask.Core.DataModel;

namespace TestTask.Classes
{
    /// <summary> 
    /// Класс, который наследует от JsonConverter и реализует методы для сериализации и десериализации перечисления Gender 
    /// </summary>
    internal class StringEnumConverter : JsonConverter<Gender>
    {
        /// <summary> 
        /// Метод, который читает строковое значение из Json и преобразует его в элемент перечисления Gender 
        /// </summary> 
        /// <param name=“reader”>Объект, который предоставляет доступ к Json</param> 
        /// <param name=“typeToConvert”>Тип, который нужно преобразовать</param> 
        /// <param name=“options”>Опции для сериализации и десериализации</param> 
        /// <returns>Элемент перечисления Gender, соответствующий строковому значению</returns> 
        public override Gender Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            string? value = reader.GetString(); return Enum.Parse<Gender>(value, true);
        }

        /// <summary>
        /// Метод, который записывает элемент перечисления Gender в Json в виде строки
        /// </summary>
        /// <param name="writer">Объект, который позволяет записывать в Json</param>
        /// <param name="value">Значение, которое нужно записать</param>
        /// <param name="options">Опции для сериализации и десериализации</param>
        public override void Write(Utf8JsonWriter writer, Gender value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString());
        }

    }
}
