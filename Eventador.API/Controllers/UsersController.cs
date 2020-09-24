using Eventador.API.Requests;
using Eventador.Domain;
using Eventador.Domain.Requests;
using Eventador.Services.Contract;
using Eventador.Services.Contract.Api;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Eventador.API.Controllers
{
    /// <summary>
    /// Контроллер пользователей
    /// </summary>
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class UsersController : BaseController
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
        /// Получить пользователя по токену(т.е. свои)
        /// </summary>
        /// <returns></returns>
        [HttpGet("Token")]
        public async Task<UserResponseModel> GetByToken()
        {
            var user = await _userService.GetById(UserId);
            var model = UserResponseModel.Create(user);

            return model;
        }

        /// <summary>
        /// Получить пользователя
        /// </summary>
        /// <param name="request">Запрос на регистрацию</param>
        /// <returns></returns>
        [HttpPost("SignUp")]
        [AllowAnonymous]
        public async Task<ActionResult> SignUp(CredentialsRequest request)
        {
            var user = await _userService.GetByLogin(request.Login);
            if(user != null)
            {
                return BadRequest("User already exist");
            }

            var newUser = new User 
            {
                LastName = request.Lastname,
                Firstname = request.Firstname,
                AboutInfo = request.Email,
                Login = request.Login,
                Password = request.Password// TODO: сделать hash

            };

            await _userService.Add(newUser);

            return Ok();
        }

        /// <summary>
        /// Получить пользователя
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<UserResponseModel> Get(long id)
        {
            var user = await _userService.GetById(id);
            var model = UserResponseModel.Create(user);

            return model;
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
        [HttpPost("{id}/Subscribe")]
        public async Task<IActionResult> Subscribe(long id)
        {
            return Ok();
        }

        /// <summary>
        /// Отписаться
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost("{id}/Unsubscribe")]
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
