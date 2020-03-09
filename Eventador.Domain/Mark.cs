using System;
using System.ComponentModel.DataAnnotations.Schema;
using Eventador.Domain.Requests;

namespace Eventador.Domain
{
    /// <summary>
    /// Оценки
    /// </summary>
    [Table("marks")]
    public class Mark
    {
        /// <summary>
        ///
        /// </summary>
        [Column("id")]
        public long Id { get; set; }

        /// <summary>
        ///
        /// </summary>
        [Column("value")]
        public int Value { get; set; }

        /// <summary>
        ///
        /// </summary>
        [Column("comment")]
        public string Comment { get; set; }

        /// <summary>
        ///
        /// </summary>
        [Column("create_date")]
        public DateTime CreateDate { get; set; }

        public static Mark CreateFromRequest(RateEventCreateRequest request)
        {
            return new Mark
            {
                Comment = request.Comment,
                Value = request.Mark
            };
        }
    }
}
