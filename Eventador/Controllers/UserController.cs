using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Eventador.API.Services.Contract.Api;

namespace Eventador.API.Controllers
{
    /// <summary>
    ///
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<UserResponseModel> Get()
        {
            var model = new UserResponseModel()
            {
                Name = "ASDASDASDD"
            };

            return model;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create()
        {
            return Ok();
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> Update()
        {
            return Ok();
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        [HttpPost("Block")]
        public async Task<IActionResult> Block()
        {
            return Ok();
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        [HttpPost("Unblock")]
        public async Task<IActionResult> Unblock()
        {
            return Ok();
        }
    }
}
