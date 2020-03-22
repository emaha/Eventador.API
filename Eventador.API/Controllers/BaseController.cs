using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Eventador.API.Controllers
{
    /// <summary>
    /// Базовый контроллер с содержанием информации о пользователе
    /// </summary>
    public class BaseController : ControllerBase
    {
        /// <summary>
        /// Имя пользователя
        /// </summary>
        public string Username => User.Identity.Name;

        /// <summary>
        /// Id пользователя
        /// </summary>
        public long UserId => long.Parse(User.Claims.Where(x => x.Type == "id").FirstOrDefault().Value);
    }
}
