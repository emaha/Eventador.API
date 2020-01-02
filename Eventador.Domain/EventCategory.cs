using System.ComponentModel.DataAnnotations.Schema;

namespace Eventador.Domain
{
    /// <summary>
    /// Категория события
    /// </summary>
    [Table("event_categories")]
    public class EventCategory
    {
        /// <summary>
        /// ID
        /// </summary>
        [Column("id")]
        public long Id { get; set; }

        /// <summary>
        /// Наименование категории
        /// </summary>
        [Column("name")]
        public string Name { get; set; }

        /// <summary>
        /// Описание категории
        /// </summary>
        [Column("description")]
        public string Description { get; set; }
    }
}
