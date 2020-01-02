using Eventador.Domain.Types;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eventador.Domain
{
    /// <summary>
    /// Событие
    /// </summary>
    [Table("events")]
    public class Event
    {
        /// <summary>
        /// ID
        /// </summary>
        [Column("id")]
        public long Id { get; set; }

        /// <summary>
        /// Заголовок
        /// </summary>
        [Column("title")]
        public string Title { get; set; }

        /// <summary>
        /// Описание
        /// </summary>
        [Column("description")]
        public string Description { get; set;}


        /// <summary>
        /// Id категорий к которым  относится событие
        /// </summary>
        [Column("category_ids")]
        public long[] CategoryIds { get; set; }
        public EventCategory[] Categories { get; set; }

        /// <summary>
        /// Дата начала
        /// </summary>
        [Column("start_date")]
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Дата окончания
        /// </summary>
        [Column("end_date")]
        public DateTime EndDate { get; set; }

        /// <summary>
        /// Широта
        /// </summary>
        [Column("lat")]
        public float Lat { get; set; }

        /// <summary>
        /// Долгота
        /// </summary>
        [Column("lon")]
        public float Lon { get; set; }
        //public string Route{get;set;} // набор точек, путь

        /// <summary>
        /// Тип доступа (видимости)
        /// </summary>
        [Column("access_type")]
        public AccessType AccessType { get; set; }

        /// <summary>
        /// Дополнительная информация
        /// </summary>
        [Column("additional_info")]
        public string AdditionalInfo { get; set; }


    }
}
