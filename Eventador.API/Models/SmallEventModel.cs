using Eventador.Domain;
using System;

namespace Eventador.API.Models
{
    /// <summary>
    /// Упрощенная модель события для представления в общем списке событий (при поиске)
    /// </summary>
    public class SmallEventModel
    {
        /// <summary>
        /// Id
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
        /// Дата начала
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Заголовочная картинка
        /// </summary>
        public string TitleImageUrl { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public long AuthorId { get; set; }

        /// <summary>
        /// Создать из запроса
        /// </summary>
        /// <param name="evnt"></param>
        /// <returns></returns>
        public static SmallEventModel Create(Event evnt)
        {
            return new SmallEventModel
            {
                Id = evnt.Id,
                Title = evnt.Title,
                Description = evnt.Description,
                StartDate = evnt.StartDate,
                TitleImageUrl = evnt.TitleImageUrl,
                AuthorId = evnt.AuthorId
            };
        }
    }
}
