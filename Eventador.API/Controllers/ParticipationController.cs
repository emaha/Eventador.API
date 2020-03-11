using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Eventador.Domain;
using Eventador.Services.Contract;
using Eventador.Services.Contract.Api;

namespace Eventador.API.Controllers
{
    // getIn leave

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
        /// Получить список людей зарегистрированных на событие по Id события
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
        /// Зарегистрироваться на событие
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost("Register/{id}")]
        public async Task<IActionResult> Register(long id)
        {
            Participation participation = new Participation();
            // TODO: Create
            await _participationService.Add(participation);

            return Ok();
        }

        /// <summary>
        /// Отписаться от события
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost("Unregister/{id}")]
        public async Task<IActionResult> Unregister(long id)
        {
            await _participationService.Delete(id);
            // TODO: Delete

            return Ok();
        }
    }
}
