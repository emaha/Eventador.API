using Eventador.Domain;
using Eventador.Domain.Requests;
using Eventador.Models;
using Eventador.Services.Contract;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Eventador.Controllers
{
    /// <summary>
    ///
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class EventController : ControllerBase
    {
        private readonly IEventService _eventService;

        /// <summary>
        ///
        /// </summary>
        /// <param name="eventService"></param>
        public EventController(IEventService eventService)
        {
            _eventService = eventService;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="eventId"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<Event> Get(int eventId)
        {
            var evnt = await _eventService.GetById(eventId);
            return evnt;
        }

        /// <summary>
        /// Получить события в регионе
        /// </summary>
        /// <returns></returns>
        [HttpGet("All")]
        public async Task<EventModel[]> GetEvents()
        {
            var events = await _eventService.GetAll();

            List<EventModel> model = new List<EventModel>();

            foreach (var item in events)
            {
                model.Add(new EventModel { Title = item.Title });
            }

            return model.ToArray();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateEvent(EventCreateRequest request)
        {
            var createdEvent = Event.CreateFromRequest(request);
            await _eventService.Add(createdEvent);

            return Ok();
        }
    }
}
