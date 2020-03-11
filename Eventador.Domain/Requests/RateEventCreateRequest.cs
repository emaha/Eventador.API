namespace Eventador.Domain.Requests
{
    /// <summary>
    /// Запрос на создание оценки
    /// </summary>
    public class RateEventCreateRequest
    {
        public long Id { get; set; }

        public int Mark { get; set; }

        public string Comment { get; set; }
    }
}
