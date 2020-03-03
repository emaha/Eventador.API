using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Eventador.Services.Contract.Api;

namespace Eventador.API.Controllers
{
    /// <summary>
    /// Контроллер пользователей
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        /// <summary>
        /// Получить пользователя
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<UserResponseModel> Get(int id)
        {
            return null;
        }

        /// <summary>
        /// Создать пользователя
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create()
        {
            return Ok();
        }

        /// <summary>
        /// Обновить данные пользователя
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> Update()
        {
            return Ok();
        }

        /// <summary>
        /// Заблокировать пользователя
        /// </summary>
        /// <returns></returns>
        [HttpPost("Block")]
        public async Task<IActionResult> Block()
        {
            return Ok();
        }

        /// <summary>
        /// Разблокировать пользователя
        /// </summary>
        /// <returns></returns>
        [HttpPost("Unblock")]
        public async Task<IActionResult> Unblock()
        {
            return Ok();
        }

        /// <summary>
        /// Подписаться
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost("Subscriber/{id}")]
        public async Task<IActionResult> Subscribe(int id)
        {
            return Ok();
        }

        /// <summary>
        /// Отписаться
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost("Unsubscribe/{id}")]
        public async Task<IActionResult> Unsubscribe(int id)
        {
            return Ok();
        }
    }
}
