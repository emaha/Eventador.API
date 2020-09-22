using System;
using System.ComponentModel.DataAnnotations.Schema;
using Eventador.Domain.Requests;
using Eventador.Domain.Statuses;
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
        ///
        /// </summary>
        [Column("title_image_url")]
        public string TitleImageUrl { get; set; }

        /// <summary>
        ///
        /// </summary>
        [Column("image_urls")]
        public string[] ImageUrls { get; set; }

        /// <summary>
        /// Id категорий к которым  относится событие
        /// </summary>
        [Column("type")]
        public EventType Type { get; set; }

        /// <summary>
        /// Статус события
        /// </summary>
        [Column("status")]
        public EventStatus Status { get; set; }

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
        /// Стоимость входа/билета/участия/депозит
        /// </summary>
        [Column("price")]
        public decimal Price { get; set; }

        /// <summary>
        /// Контактные номера телефонов
        /// </summary>
        //public string[] ContactPhone { get; set; }

        /// <summary>
        /// Регион
        /// </summary>
        [Column("region_id")]
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
        /// Дата обновления
        /// </summary>
        [Column("change_date")]
        public DateTime ChangeDate { get; set; }

        /// <summary>
        /// Id аккаунта создателя
        /// </summary>
        [Column("author_id")]
        public long AuthorId { get; set; }

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
                AccessType = request.AccessType,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                RegionId = request.RegionId,
                Price = request.Price,
                Lat = request.Lat,
                Lon = request.Lon,
                Type = request.EventType,
                Status = EventStatus.Active,
                CreateDate = DateTime.UtcNow,
                ChangeDate = DateTime.UtcNow,
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
            AccessType = request.AccessType;
            StartDate = request.StartDate;
            EndDate = request.EndDate;
            //RegionId = request.RegionId;
            Price = request.Price;
            Lat = request.Lat;
            Lon = request.Lon;
            Type = request.EventType;

            ChangeDate = DateTime.UtcNow;

            // TODO:
        }

        /// <summary>
        /// Активировать
        /// </summary>
        public void Activate()
        {
            Status = EventStatus.Active;
            ChangeDate = DateTime.UtcNow;
        }

        /// <summary>
        /// Завершить
        /// </summary>
        public void Finish()
        {
            Status = EventStatus.Finished;
            ChangeDate = DateTime.UtcNow;
        }

        /// <summary>
        /// Приостановить
        /// </summary>
        public void Suspend()
        {
            Status = EventStatus.Suspended;
            ChangeDate = DateTime.UtcNow;
        }

        /// <summary>
        /// Отменить (не удаление)
        /// </summary>
        public void Cancel()
        {
            Status = EventStatus.Canceled;
            ChangeDate = DateTime.UtcNow;
        }
    }
}
