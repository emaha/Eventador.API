using Eventador.Domain.Requests;
using Eventador.Services.Contract;
using Eventador.Services.Contract.Api;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Eventador.API.Controllers
{
    /// <summary>
    /// Контроллер пользователей
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="userService"></param>
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Получить пользователя
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<UserResponseModel> Get(long id)
        {
            return null;
        }

        /// <summary>
        /// Создать пользователя
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create(UserCreateRequest request)
        {
            var user = Domain.User.Create(request);
            await _userService.Add(user);

            return Ok();
        }

        /// <summary>
        /// Обновить данные пользователя
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> Update(UserUpdateRequest request)
        {
            var user = await _userService.GetById(request.Id);
            if (user == null) return NotFound();

            user.Update(request);
            await _userService.SaveChanges();

            return Ok();
        }

        /// <summary>
        /// Подписаться
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost("Subscriber/{id}")]
        public async Task<IActionResult> Subscribe(long id)
        {
            return Ok();
        }

        /// <summary>
        /// Отписаться
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost("Unsubscribe/{id}")]
        public async Task<IActionResult> Unsubscribe(long id)
        {
            return Ok();
        }

        /// <summary>
        /// Заблокировать пользователя
        /// </summary>
        /// <returns></returns>
        [HttpPost("{id}/Block")]
        public async Task<IActionResult> BlockUser(long id)
        {
            return Ok();
        }

        /// <summary>
        /// Разблокировать пользователя
        /// </summary>
        /// <returns></returns>
        [HttpPost("{id}/Unblock")]
        public async Task<IActionResult> UnblockUser(long id)
        {
            return Ok();
        }
    }
}
