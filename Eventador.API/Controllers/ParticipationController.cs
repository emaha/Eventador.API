using Eventador.API.Models;
using Eventador.Domain;
using Eventador.Services.Contract;
using Eventador.Services.Contract.Api;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace Eventador.API.Controllers
{
    /// <summary>
    /// Контроллер участия (добавление в список участников события)
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class ParticipationController : ControllerBase
    {
        private readonly IParticipationService _participationService;
        private readonly IEventService _eventService;

        /// <summary>
        /// ctor
        /// </summary>
        public ParticipationController(
            IEventService eventService,
            IParticipationService participationService
            )
        {
            _eventService = eventService;
            _participationService = participationService;
        }

        /// <summary>
        /// Получить список людей зарегистрированных на событие по его Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("Event/{id}")]
        public async Task<UserResponseModel[]> GetByEventId(long id)
        {
            var participations = await _participationService.GetByEventId(id);

            var users = participations.Select(x => new UserResponseModel
            {
                Id = x.Id
            }).ToArray();

            return users;
        }

        /// <summary>
        /// Получить события в которых участвует пользователь
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("User/{id}")]
        public async Task<SmallEventModel[]> GetByUserId(long id)
        {
            var participations = await _participationService.GetByUserId(id);
            var events = await _eventService.GetByIds(participations.Select(x => x.Id).ToArray());
            var models = events.Select(SmallEventModel.Create).ToArray();

            return models;
        }

        /// <summary>
        /// Зарегистрироваться на событие
        /// </summary>
        /// <param name="eventId"></param>
        /// <returns></returns>
        [HttpPost("Register/{eventId}")]
        public async Task<IActionResult> Register(long eventId)
        {
            long userId = 1; //TODO:
            var participations = await _participationService.GetByEventId(eventId);
            if (participations.Select(x => x.UserId).Contains(userId))
            {
                return Ok("Already registered");
            }

            var participation = Participation.Create(eventId, userId);
            await _participationService.Add(participation);

            return Ok("Successful registered");
        }

        /// <summary>
        /// Отписаться от события
        /// </summary>
        /// <param name="eventId"></param>
        /// <returns></returns>
        [HttpPost("Unregister/{eventId}")]
        public async Task<IActionResult> Unregister(long eventId)
        {
            long userId = 1; //TODO:
            var participations = await _participationService.GetByEventId(eventId);
            var participation = participations.FirstOrDefault(x => x.UserId == userId);

            if (participation == null)
            {
                return Ok("Not registered");
            }

            await _participationService.Delete(participation.Id);
            return Ok("Successful unregistered");
        }
    }
}
