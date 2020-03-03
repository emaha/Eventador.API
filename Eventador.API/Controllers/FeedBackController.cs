using System;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Eventador.Domain;
using Eventador.Domain.Requests;

namespace Eventador.API.Controllers
{
    /// <summary>
    /// Контроллер обратной связи
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class FeedBackController : ControllerBase
    {
        /// <summary>
        /// Оценить событие
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("RateEvent")]
        public async Task<IActionResult> RateEvent(RateEventCreateRequest request)
        {
            Mark mark = Mark.CreateFromRequest(request);

            // TODO: проверить на уже существующие оценки и добавление оценки

            return Ok();
        }

        /// <summary>
        /// Получить рейтинг пользователя
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("Rating/{id}")]
        public async Task<double> GetPersonRating(int id)
        {
            return new Random().NextDouble() * 10.0f;
        }

        /// <summary>
        /// Пожаловаться на событие
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("ComplainEvent")]
        public async Task<IActionResult> ComplainEvent(ComplaintCreateRequest request)
        {
            return Ok();
        }

        /// <summary>
        /// Пожаловаться на пользователя
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("ComplainPerson")]
        public async Task<IActionResult> ComplainPerson(ComplaintCreateRequest request)
        {
            return Ok();
        }
    }
}
