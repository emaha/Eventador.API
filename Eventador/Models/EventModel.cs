using Eventador.Domain;

namespace Eventador.Models
{
    /// <summary>
    /// Упрощенная модель события
    /// </summary>
    public class EventModel
    {
        /// <summary>
        /// Заголовок
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="evnt"></param>
        /// <returns></returns>
        public static EventModel Create(Event evnt)
        {
            return new EventModel
            {
                Title = evnt.Title
            };
        }
    }
}
