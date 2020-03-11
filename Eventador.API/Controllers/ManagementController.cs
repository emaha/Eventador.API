using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Eventador.API.Controllers
{
    /// <summary>
    /// Контроллер управления
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class ManagementController : ControllerBase
    {
        /// <summary>
        /// Заблокировать пользователя
        /// </summary>
        /// <returns></returns>
        [HttpPost("BlockUser/{id}")]
        public async Task<IActionResult> BlockUser(long id)
        {
            return Ok();
        }

        /// <summary>
        /// Разблокировать пользователя
        /// </summary>
        /// <returns></returns>
        [HttpPost("UnblockUser/{id}")]
        public async Task<IActionResult> UnblockUser(long id)
        {
            return Ok();
        }

        /// <summary>
        /// Заблокировать событие
        /// </summary>
        /// <returns></returns>
        [HttpPost("BlockEvent/{id}")]
        public async Task<IActionResult> BlockEvent(long id)
        {
            return Ok();
        }

        /// <summary>
        /// Разблокировать событие
        /// </summary>
        /// <returns></returns>
        [HttpPost("UnblockEvent/{id}")]
        public async Task<IActionResult> UnblockEvent(long id)
        {
            return Ok();
        }
    }
}
