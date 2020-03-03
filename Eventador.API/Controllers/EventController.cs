using Eventador.API.Models;
using Eventador.Services.Contract;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using Eventador.Domain;
using Eventador.Domain.Requests;

namespace Eventador.API.Controllers
{
    /// <summary>
    /// Контроллер событий
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class EventController : ControllerBase
    {
        private readonly IEventService _eventService;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="eventService"></param>
        public EventController(IEventService eventService)
        {
            _eventService = eventService;
        }

        /// <summary>
        /// Получить по id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<EventModel> GetById(int id)
        {
            var evnt = await _eventService.GetById(id);
            var model = EventModel.Create(evnt);

            return model;
        }

        /// <summary>
        /// Получить события в регионе
        /// </summary>
        /// <returns></returns>
        [HttpGet("All")]
        public async Task<SmallEventModel[]> GetEvents()
        {
            //TODO: получить текущий регион для местоположения
            int regionId = 1;

            var events = await _eventService.GetByRegion(regionId);

            return events.Select(SmallEventModel.Create).ToArray();
        }

        /// <summary>
        /// Создание события
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateEvent(EventCreateRequest request)
        {
            var createdEvent = Event.Create(request);
            await _eventService.Add(createdEvent);

            return Ok();
        }

        /// <summary>
        /// Изменение события
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> UpdateEvent(EventUpdateRequest request)
        {
            var evnt = await _eventService.GetById(request.Id);
            if (evnt == null) return NotFound();

            evnt.Update(request);

            await _eventService.Update(evnt);

            return Ok();
        }

        /// <summary>
        /// Завершение события
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost("Finish")]
        public async Task<IActionResult> FinishEvent(int id)
        {
            var evnt = await _eventService.GetById(id);
            evnt.Finish();

            return Ok();
        }
    }
}
