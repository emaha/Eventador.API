﻿using Eventador.Domain;

namespace Eventador.API.Models
{
    /// <summary>
    /// Упрощенная модель события
    /// </summary>
    public class SmallEventModel
    {
        /// <summary>
        ///
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Заголовок
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Описание
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string TitleImageUrl { get; set; }

        /// <summary>
        ///
        /// </summary>
        /// <param name="evnt"></param>
        /// <returns></returns>
        public static SmallEventModel Create(Event evnt)
        {
            return new SmallEventModel
            {
                Title = evnt.Title,
                Description = evnt.Description,
                TitleImageUrl = evnt.TitleImageUrl
            };
        }
    }
}