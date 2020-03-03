using System;
using System.ComponentModel.DataAnnotations.Schema;
using Eventador.Domain.Requests;
using Eventador.Domain.Types;

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
        public string Description { get; set; }

        /// <summary>
        /// Дополнительная информация
        /// </summary>
        [Column("additional_info")]
        public string AdditionalInfo { get; set; }

        /// <summary>
        /// Id категорий к которым  относится событие
        /// </summary>
        [Column("event_type")]
        public EventType EventType { get; set; }

        /// <summary>
        /// Статус события
        /// </summary>
        [Column("event_status")]
        public EventStatus EventStatus { get; set; }

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
        /// Регион
        /// </summary>
        public int RegionId { get; set; }

        /// <summary>
        /// Широта
        /// </summary>
        [Column("lat")]
        public float? Lat { get; set; }

        /// <summary>
        /// Долгота
        /// </summary>
        [Column("lon")]
        public float? Lon { get; set; }

        /// <summary>
        /// Дата создания
        /// </summary>
        [Column("create_date")]
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// Id аккаунта создателя
        /// </summary>
        [Column("author_id")]
        public int AuthorId { get; set; }

        //public string Route{get;set;} // набор точек, путь

        /// <summary>
        /// Тип доступа (видимости)
        /// </summary>
        [Column("access_type")]
        public AccessType AccessType { get; set; }

        public static Event Create(EventCreateRequest request)
        {
            //TODO: author id
            int authorId = 0;

            return new Event
            {
                Title = request.Title,
                Description = request.Description,
                AdditionalInfo = request.AdditionalInfo,
                AccessType = request.AccessType,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                Lat = request.Lat,
                Lon = request.Lon,
                EventType = request.EventType,
                EventStatus = EventStatus.ACTIVE,
                CreateDate = DateTime.UtcNow,
                AuthorId = authorId
            };
        }

        /// <summary>
        /// Обновить
        /// </summary>
        /// <param name="request"></param>
        public void Update(EventUpdateRequest request)
        {
            if (request == null) return;

            Title = request.Title;
            Description = request.Description;

            // TODO:
        }

        /// <summary>
        /// Завершить
        /// </summary>
        public void Finish()
        {
            EventStatus = EventStatus.FINISHED;
        }
    }
}
