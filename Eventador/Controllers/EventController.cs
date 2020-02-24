using Eventador.Models;
using Eventador.Requests;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Eventador.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EventController : ControllerBase
    {
        private static readonly string[] eventStrings = new[]
        {
            "Кибер турнир","Чья-то днюха","Пикник","Летский утренник"
        };

        [HttpGet]
        public IActionResult Get()
        {
            var txt = eventStrings[new Random().Next(eventStrings.Length)];
            return Ok(txt);
        }

        /// <summary>
        /// Получить события в регионе
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<EventModel[]> GetEvent()
        {
            List<EventModel> model = new List<EventModel>();

            foreach (var item in eventStrings)
            {
                model.Add(new EventModel { Title = item });
            }

            return model.ToArray();
        }

        [HttpPost]
        public async Task<IActionResult> CreateEvent(CreateEventRequest request)
        {

            return Ok();
        }
    }
}
