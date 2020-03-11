using Eventador.Domain.Types;

namespace Eventador.Domain.Requests
{
    /// <summary>
    /// Запрос на создание жалобы
    /// </summary>
    public class ComplaintCreateRequest
    {
        /// <summary>
        /// Id сущности(субъекта)
        /// </summary>
        public long SubjectId { get; set; }

        /// <summary>
        /// Тип жалобы
        /// </summary>
        public ComplaintType ComplaintType { get; set; }

        /// <summary>
        /// Текст
        /// </summary>
        public string ReasonText { get; set; }
    }
}
