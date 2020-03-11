using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eventador.Domain
{
    /// <summary>
    /// Участия в событие
    /// </summary>
    [Table("participations")]
    public class Participation
    {
        /// <summary>
        /// Id
        /// </summary>
        [Column("id")]
        public long Id { get; set; }

        /// <summary>
        /// Пользователь
        /// </summary>
        [Column("user_id")]
        public int UserId { get; set; }

        /// <summary>
        /// Событие
        /// </summary>
        [Column("event_id")]
        public long EventId { get; set; }

        /// <summary>
        /// Дата создания
        /// </summary>
        [Column("create_date")]
        public DateTime CreateDate { get; set; }
    }
}
