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
        public long UserId { get; set; }

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

        /// <summary>
        ///
        /// </summary>
        /// <param name="eventId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static Participation Create(long eventId, long userId)
        {
            return new Participation()
            {
                EventId = eventId,
                UserId = userId,
                CreateDate = DateTime.UtcNow
            };
        }
    }
}
