using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Eventador.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("ASD");
        }

        public async Task<IActionResult> Create()
        {
            return Ok();
        }

        public async Task<IActionResult> Update()
        {
            return Ok();
        }

        public async Task<IActionResult> Block()
        {
            return Ok();
        }

        public async Task<IActionResult> Unblock()
        {
            return Ok();
        }
    }
}
