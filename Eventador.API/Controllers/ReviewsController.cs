using System;
using System.Runtime.InteropServices.ComTypes;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Eventador.Domain;
using Eventador.Domain.Requests;
using Eventador.Services.Contract;

namespace Eventador.API.Controllers
{
    /// <summary>
    /// Контроллер обратной связи
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class ReviewsController : ControllerBase
    {
        private readonly IMarkService _markService;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="markService"></param>
        public ReviewsController(IMarkService markService)
        {
            _markService = markService;
        }

        /// <summary>
        /// Оценить событие
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("RateEvent")]
        public async Task<IActionResult> RateEvent(RateEventCreateRequest request)
        {
            Mark mark = Mark.Create(request);

            // TODO: проверить на уже существующие оценки и добавление оценки

            return Ok();
        }

        /// <summary>
        /// Получить отзывы о событии
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("Event/{id}")]
        public async Task<string[]> GetEventFeedback(long id)
        {
            // TODO: need pagination
            return new string[] { };
        }

        /// <summary>
        /// Получить рейтинг пользователя
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("PersonRating/{id}")]
        public async Task<double> GetPersonRating(long id)
        {
            return new Random().NextDouble() * 10.0f;
        }

        /// <summary>
        /// Получить рейтинг события
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("EventRating/{id}")]
        public async Task<double> GetEventRating(long id)
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
