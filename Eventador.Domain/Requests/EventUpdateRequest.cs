using System;
using Eventador.Domain.Types;

namespace Eventador.Domain.Requests
{
    /// <summary>
    /// Запрос на обновление события
    /// </summary>
    public class EventUpdateRequest
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string AdditionalInfo { get; set; }
        public EventType[] EventType { get; set; }
        public AccessType AccessType { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int RegionId { get; set; }
        public float? Lat { get; set; }
        public float? Lon { get; set; }
    }
}
