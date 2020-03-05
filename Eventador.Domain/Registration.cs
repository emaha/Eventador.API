using System;
using System.ComponentModel.DataAnnotations.Schema;
using Eventador.Domain.Requests;

namespace Eventador.Domain
{
    /// <summary>
    /// Регистрации на событие
    /// </summary>
    [Table("registration")]
    public class Registration
    {
        /// <summary>
        ///
        /// </summary>
        [Column("id")]
        public int Id { get; set; }

        /// <summary>
        ///
        /// </summary>
        [Column("user_id")]
        public int UserId { get; set; }

        /// <summary>
        ///
        /// </summary>
        [Column("event_id")]
        public string EventId { get; set; }

        /// <summary>
        ///
        /// </summary>
        [Column("create_date")]
        public DateTime CreateDate { get; set; }
    }
}
