using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Mvc;

namespace Eventador.Common.JsonSerializer
{
    /// <summary>
    /// Builder опций сериализатора для Json
    /// </summary>
    public class JsonSerializer
    {
        /// <summary>
        /// Политика именования
        /// </summary>
        private JsonNamingPolicy _namingPolicy;

        /// <summary>
        /// Индикатор игнорирования пустых значений
        /// </summary>
        private bool _ignoreNullValues;

        /// <summary>
        /// Индикатор игнорирования полей для чтения
        /// </summary>
        private bool _ignoreReadOnlyProperties;

        /// <summary>
        /// Индикатор без учета регистра
        /// </summary>
        private bool _propertyNameCaseInsensitive;

        /// <summary>
        /// Коллекция для json конвертеров
        /// </summary>
        private readonly ICollection<JsonConverter> _jsonConverters = new List<JsonConverter>();

        /// <summary>
        /// Установка опций для сериализации
        /// </summary>
        /// <param name="options">Опции для сериализации</param>
        /// <returns></returns>
        public JsonOptions SetOptions(JsonOptions options)
        {
            if (_namingPolicy != null)
            {
                options.JsonSerializerOptions.DictionaryKeyPolicy = _namingPolicy;
                options.JsonSerializerOptions.PropertyNamingPolicy = _namingPolicy;
            }

            options.JsonSerializerOptions.IgnoreNullValues = _ignoreNullValues;

            options.JsonSerializerOptions.IgnoreReadOnlyProperties = _ignoreReadOnlyProperties;

            options.JsonSerializerOptions.PropertyNameCaseInsensitive = _propertyNameCaseInsensitive;

            AddConverter(options, _jsonConverters);

            return options;
        }

        /// <summary>
        /// Создать опции для Json по умолчанию
        /// </summary>
        /// <param name="jsonOptions"></param>
        public static JsonOptions CreateOptionsByDefault([NotNull] JsonOptions jsonOptions)
        {
            return new JsonSerializer()
                .WithCamelCase()
                .WithPropertyNameCaseInsensitive()
                .WithIgnoreNullValue()
                .WithIgnoreReadOnlyProperties()
                .WithStringEnumConverter()
                .WithDateTimeShortISOConverter()
                .SetOptions(jsonOptions);
        }

        /// <summary>
        /// Добавить список конвертеров
        /// </summary>
        /// <param name="options">Json опции</param>
        /// <param name="converters">Json конвертеры</param>
        private static void AddConverter(JsonOptions options, IEnumerable<JsonConverter> converters)
        {
            foreach (var jsonConverter in converters)
            {
                AddConverter(options, jsonConverter);
            }
        }

        /// <summary>
        /// Добавить конвертер
        /// </summary>
        /// <param name="options">Json опции</param>
        /// <param name="converter">Json конвертер</param>
        private static void AddConverter(JsonOptions options, JsonConverter converter)
        {
            options.JsonSerializerOptions.Converters.Add(converter);
        }

        /// <summary>
        /// Использование lowerCamelCase стиль именования
        /// </summary>
        /// <returns></returns>
        public JsonSerializer WithCamelCase()
        {
            _namingPolicy = JsonNamingPolicy.CamelCase;
            return this;
        }

        /// <summary>
        /// Игнорирование Nullable значений
        /// </summary>
        /// <returns></returns>
        public JsonSerializer WithIgnoreNullValue()
        {
            _ignoreNullValues = true;
            return this;
        }

        /// <summary>
        /// Игнорирование в сериализации полей для чтений
        /// </summary>
        /// <returns></returns>
        public JsonSerializer WithIgnoreReadOnlyProperties()
        {
            _ignoreReadOnlyProperties = true;
            return this;
        }

        /// <summary>
        /// Не учитывать регистр имени для десериализации
        /// </summary>
        /// <returns></returns>
        public JsonSerializer WithPropertyNameCaseInsensitive()
        {
            _propertyNameCaseInsensitive = true;
            return this;
        }

        /// <summary>
        /// Конвертация enum значений в string
        /// </summary>
        /// <returns></returns>
        public JsonSerializer WithStringEnumConverter()
        {
            return WithJsonConverter(new JsonStringEnumConverter());
        }

        /// <summary>
        /// Конвертация даты в формате "yyyy-MM-ddTHH:mm:sszz".
        /// </summary>
        /// <returns></returns>
        public JsonSerializer WithDateTimeShortISOConverter()
        {
            return WithJsonConverter(new DateTimeConverter(DateTimeConverter.DateFormatISO8601()));
        }

        /// <summary>
        /// Добавить конвертер
        /// </summary>
        /// <param name="jsonConverter">Конвертер</param>
        /// <returns></returns>
        public JsonSerializer WithJsonConverter(JsonConverter jsonConverter)
        {
            _jsonConverters.Add(jsonConverter);
            return this;
        }
    }
}
