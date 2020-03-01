using Eventador.Domain.Requests;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eventador.Domain
{
    /// <summary>
    /// Оценки
    /// </summary>
    [Table("rates")]
    public class Rate
    {
        /// <summary>
        /// 
        /// </summary>
        [Column("id")]
        public int Id { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        [Column("mark")]
        public int Mark { get; set; }
        
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

        public static Rate CreateFromRequest(RateEventCreateRequest request)
        {
            return new Rate
            {
                Comment = request.Comment,
                Mark = request.Mark
            };
        }
    }
}
