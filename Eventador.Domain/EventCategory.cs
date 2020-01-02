namespace Eventador.Domain
{
    /// <summary>
    /// Категория события
    /// </summary>
    public class EventCategory
    {
        /// <summary>
        /// ID
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Наименование категории
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Описание категории
        /// </summary>
        public string Description { get; set; }
    }
}