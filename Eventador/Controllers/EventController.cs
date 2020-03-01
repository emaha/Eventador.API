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
            //var events = await _eventService.GetAll();

            var model = new List<SmallEventModel>();

            foreach (var item in fakeEvents)
            {
                model.Add(new SmallEventModel { Title = item });
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
        /// 
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
        /// Завершить событие
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
