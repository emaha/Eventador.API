namespace Eventador.Domain.Requests
{
    public class RateEventCreateRequest
    {
        public long Id { get; set; }

        public int Mark { get; set; }

        public string Comment { get; set; }
    }
}
