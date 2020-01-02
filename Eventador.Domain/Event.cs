using Eventador.Domain.Types;
using System;

namespace Eventador.Domain
{
    /// <summary>
    /// Событие
    /// </summary>
    public class Event
    {
        /// <summary>
        /// ID
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Заголовок
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Описание
        /// </summary>
        public string Description { get; set;}


        /// <summary>
        /// Id категорий к которым  относится событие
        /// </summary>
        public long[] CategoryIds { get; set; }

        /// <summary>
        /// Дата начала
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Дата окончания
        /// </summary>
        public DateTime EndDate { get; set; }

        /// <summary>
        /// Широта
        /// </summary>
        public float Lat { get; set; }

        /// <summary>
        /// Долгота
        /// </summary>
        public float Lon { get; set; }
        //public string Route{get;set;} // набор точек, путь

        /// <summary>
        /// Тип доступа (видимости)
        /// </summary>
        public AccessType AccessType { get; set; }

        /// <summary>
        /// Дополнительная информация
        /// </summary>
        public string AdditionalInfo { get; set; }


    }
}
