using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Eventador.Domain;
using Eventador.Services.Contract.Api;

namespace Eventador.API.Controllers
{
    /// <summary>
    /// Контроллер регистрации
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class RegistrationController : ControllerBase
    {
        /// <summary>
        /// Получить по id события
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("Event/{id}")]
        public async Task<UserResponseModel> GetByEventId(long id)
        {
            return null;
        }

        /// <summary>
        /// Получить по id пользователя
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("User/{id}")]
        public async Task<UserResponseModel> GetByUserId(long id)
        {
            return null;
        }

        /// <summary>
        /// Зарегистрироваться на событие
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost("Register/{id}")]
        public async Task<IActionResult> Register(long id)
        {
            Registration registration = new Registration();
            // TODO: Create

            return Ok();
        }

        /// <summary>
        /// Отписаться от события
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost("UnRegister/{id}")]
        public async Task<IActionResult> UnRegister(long id)
        {
            // TODO: Delete

            return Ok();
        }
    }
}
