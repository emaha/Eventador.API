using Eventador.Domain;
using Eventador.Domain.Requests;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Eventador.Controllers
{
    /// <summary>
    ///
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class FeedBackCpontroller : ControllerBase
    {
        /// <summary>
        /// Оценить событие
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<IActionResult> RateEvent(RateEventCreateRequest request)
        {
            Rate rate = Rate.CreateFromRequest(request);

            // TODO: проверить на уже существующие оценки и добавдение оценки

            return Ok();
        }
    }
}
