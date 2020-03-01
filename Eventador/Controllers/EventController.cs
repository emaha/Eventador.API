using Eventador.Domain;
using Eventador.Domain.Requests;
using Eventador.Models;
using Eventador.Services.Contract;
using Microsoft.AspNetCore.Mvc;
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
        private string[] fakeEvents = {
            "Кибер турнир","Чья-то днюха",
            "Пикник","Детский утренник", "Стритрейсинг",
            "Велопробежка","Курсы программирования", "Совместные закупки"        
        };

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
        public async Task<EventModel> Get(int eventId)
        {
            var evnt = await _eventService.GetById(eventId);

            var model = EventModel.Create(evnt);

            return model;
        }

        /// <summary>
        /// Получить события в регионе
        /// </summary>
        /// <returns></returns>
        [HttpGet("All")]
        public async Task<EventModel[]> GetEvents()
        {
            //var events = await _eventService.GetAll();

            List<EventModel> model = new List<EventModel>();

            foreach (var item in fakeEvents)
            {
                model.Add(new EventModel { Title = item });
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
            var createdEvent = Event.Create(request);
            await _eventService.Add(createdEvent);

            return Ok();
        }

        /// <summary>
        /// Завершить событие
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> FinishEvent(int id)
        {
            var evnt = await _eventService.GetById(id);
            evnt.Finish();


            return Ok();
        }

        /// <summary>
        /// Оценить событие
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<IActionResult> RateEvent(RateEventCreateRequest request)
        {
            Rate rate = Rate.CreateFromRequest(request);

            // TODO: проверить на уже существующие оценки и добавдение оценки

            return Ok();
        }
    }
}
