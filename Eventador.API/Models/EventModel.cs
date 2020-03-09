using Eventador.Domain;
using Eventador.Domain.Statuses;
using Eventador.Domain.Types;
using System;

namespace Eventador.API.Models
{
    /// <summary>
    /// Упрощенная модель события
    /// </summary>
    public class EventModel
    {
        /// <summary>
        /// 
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Заголовок
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Описание
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Дополнительная информация
        /// </summary>
        public string AdditionalInfo { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string TitleImageUrl { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string[] ImageUrls { get; set; }

        /// <summary>
        /// Id категорий к которым  относится событие
        /// </summary>
        public EventType EventType { get; set; }

        /// <summary>
        /// Статус события
        /// </summary>
        public EventStatus EventStatus { get; set; }

        /// <summary>
        /// Дата начала
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Дата окончания
        /// </summary>
        public DateTime EndDate { get; set; }

        /// <summary>
        /// Стоимость входа/билета/участия/депозит
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Регион
        /// </summary>
        public int RegionId { get; set; }

        /// <summary>
        /// Широта
        /// </summary>
        public float? Lat { get; set; }

        /// <summary>
        /// Долгота
        /// </summary>
        public float? Lon { get; set; }

        /// <summary>
        /// Дата создания
        /// </summary>
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// Id аккаунта создателя
        /// </summary>
        public int AuthorId { get; set; }

        //public string Route{get;set;} // набор точек, путь

        /// <summary>
        /// Тип доступа (видимости)
        /// </summary>
        public AccessType AccessType { get; set; }

        /// <summary>
        ///
        /// </summary>
        /// <param name="evnt"></param>
        /// <returns></returns>
        public static EventModel Create(Event evnt)
        {
            if (evnt == null) return null;

            return new EventModel
            {
                Id = evnt.Id,
                Title = evnt.Title
            };
        }
    }
}
