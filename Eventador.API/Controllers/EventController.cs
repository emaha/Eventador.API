using Eventador.API.Models;
using Eventador.Services.Contract;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using Eventador.Domain;
using Eventador.Domain.Requests;
using Microsoft.AspNetCore.Authorization;

namespace Eventador.API.Controllers
{
    /// <summary>
    /// Контроллер событий
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    //[Authorize]
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
        public async Task<EventModel> GetById(long id)
        {
            var evnt = await _eventService.GetById(id);
            var model = EventModel.Create(evnt);

            return model;
        }

        /// <summary>
        /// Получить события в регионе
        /// </summary>
        /// <returns></returns>
        [HttpGet("Region/{id}")]
        public async Task<SmallEventModel[]> GetEventsByRegion(long id)
        {
            var events = await _eventService.GetByRegion(id);

            return events.Select(SmallEventModel.Create).ToArray();
        }

        /// <summary>
        /// Получить события определённого пользователя
        /// </summary>
        /// <returns></returns>
        [HttpGet("Author/{id}")]
        public async Task<SmallEventModel[]> GetEventsByAuthor(long id)
        {
            var events = await _eventService.GetByAuthorId(id);

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
        /// Удаление события
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEvent(long id)
        {
            await _eventService.Delete(id);
            return Ok();
        }

        /// <summary>
        /// Завершение события
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost("{id}/Finish")]
        public async Task<IActionResult> FinishEvent(long id)
        {
            var evnt = await _eventService.GetById(id);
            evnt.Finish();

            return Ok();
        }
    }
}
