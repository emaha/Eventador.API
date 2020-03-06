using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Eventador.Common.JsonSerializer
{
    /// <summary>
    /// Конвертер для даты
    /// </summary>
    public class DateTimeConverter : JsonConverter<DateTime>
    {
        /// <summary>
        /// Формат даты для конвертации
        /// </summary>
        private readonly string _dateFormat;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="dateFormat"></param>
        public DateTimeConverter(string dateFormat)
        {
            _dateFormat = dateFormat;
        }

        /// <inheritdoc />
        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return DateTime.Parse(reader.GetString());
        }

        /// <inheritdoc />
        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString(_dateFormat));
        }

        /// <summary>
        /// Формат даты и времени по ISO 8601 с тайм зоной в зависимости от DateTimeKind.
        /// <remarks>Формат даты "yyyy-MM-ddTHH:mm:sszz". zz - тайм зона, если есть Z или +03</remarks>
        /// </summary>
        /// <returns></returns>
        public static string DateFormatISO8601()
        {
            return "yyyy'-'MM'-'dd'T'HH':'mm':'sszz";
        }
    }
}
