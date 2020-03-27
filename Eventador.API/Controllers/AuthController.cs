using Eventador.API.Requests;
using Eventador.API.Services;
using Eventador.Domain;
using Eventador.Domain.Models;
using Eventador.Services.Contract;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Eventador.API.Controllers
{
    /// <summary>
    /// Контроллер авторизации
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class AuthController : BaseController
    {
        private readonly IUserService _userService;
        private readonly ITokenWithRefreshService _tokenWithRefreshService;
        private readonly ILogger<AuthController> _logger;

        /// <summary>
        /// ctor
        /// </summary>
        public AuthController(
            ITokenWithRefreshService tokenWithRefreshWithRefreshService,
            ILogger<AuthController> logger,
            IUserService userService
            )
        {
            _tokenWithRefreshService = tokenWithRefreshWithRefreshService;
            _logger = logger;
            _userService = userService;
        }

        /// <summary>
        /// Вход
        /// </summary>
        /// <param name="request"></param>
        /// <returns>Токен</returns>
        [HttpPost("SignIn")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<TokenModel>> SignIn(CredentialsRequest request)
        {
            var identity = await GetIdentity(request.Username, request.Password);

            if (identity == null)
            {
                return Problem("Не валидный логин или пароль", statusCode: StatusCodes.Status400BadRequest, title: "Bad Request");
            }

            var token = _tokenWithRefreshService.GetToken(identity);

            return token;
        }

        /// <summary>
        /// Выход со всех устройств
        /// </summary>
        [Authorize]
        [HttpPost("SignOut")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<TokenModel> SignOut()
        {
            var claimLogin = GetClaimLogin(User.Claims);

            if (claimLogin == null)
            {
                _logger.LogWarning("Не удалось получить идентификационных данные для логина пользователя.");
                return Unauthorized();
            }

            var login = claimLogin.Value;
            _tokenWithRefreshService.DeleteAllRefreshToken(login);
            return Ok();
        }

        /// <summary>
        /// Обновить токен
        /// </summary>
        /// <param name="request">Запрос на обновление токена из refresh token</param>
        /// <returns></returns>
        [HttpPost("RefreshToken")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<TokenModel>> RefreshToken(RefreshTokenRequest request)
        {
            JwtSecurityToken refreshTokenOld;
            try
            {
                refreshTokenOld = new JwtSecurityToken(request.RefreshToken);
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning(ex, "Не удалось получить данные из RefreshToken");
                return BadRequest();
            }

            // Refresh токен истек
            //
            if (DateTime.UtcNow > refreshTokenOld.ValidTo)
            {
                _logger.LogWarning("Refresh token истек");
                return Unauthorized();
            }

            var claimLogin = GetClaimLogin(refreshTokenOld);

            if (claimLogin == null)
            {
                _logger.LogWarning("Нет идентификационных данных в токене для логина пользователя");
                return BadRequest();
            }

            var login = claimLogin.Value;

            if (!_tokenWithRefreshService.ValidRefreshToken(login, request.RefreshToken))
            {
                _logger.LogWarning($"Для логина {login} не совпадает RefreshToken");
                return Unauthorized();
            }

            // Идентификационные данные не найдены
            //
            var identity = await GetIdentity(login);

            if (identity == null)
            {
                _logger.LogWarning("Идентификационные данные не найдены");
                return BadRequest();
            }

            var token = _tokenWithRefreshService.GetTokenByRefreshToken(identity, request.RefreshToken);

            return token;
        }

        /// <summary>
        /// Восстановление пароля
        /// </summary>
        /// <param name="request">Запрос на восстановление пароля</param>
        /// <returns></returns>
        [HttpPost("RecoverPassword")]
        public async Task<IActionResult> RecoverPassword(RecoverPasswordRequest request)
        {
            var user = await _userService.GetByLogin(request.Username);
            if (user == null)
            {
                _logger.LogWarning("Пользователь не найден");
                return BadRequest();
            }

            user.Password = GeneratePassword(8);
            await _userService.SaveChanges();

            // TODO: recover
            // send email

            _tokenWithRefreshService.DeleteAllRefreshToken(request.Username);

            return Ok();
        }

        private string GeneratePassword(int length)
        {
            // TODO:

            return "12345678";
        }

        /// <summary>
        /// Получить идентификационные данные для логина
        /// </summary>
        /// <param name="refreshToken">Refresh token</param>
        /// <returns></returns>
        private static Claim GetClaimLogin(JwtSecurityToken refreshToken)
        {
            return GetClaimLogin(refreshToken.Claims);
        }

        /// <summary>
        /// Получить идентификационные данные для логина
        /// </summary>
        /// <param name="claims"></param>
        /// <returns></returns>
        private static Claim GetClaimLogin(IEnumerable<Claim> claims)
        {
            return claims.FirstOrDefault(x => x.Type == "login");
        }

        /// <summary>
        /// Получить идентификационные данные
        /// </summary>
        /// <param name="login">Логин</param>
        /// <returns></returns>
        private async Task<ClaimsIdentity> GetIdentity(string login)
        {
            var user = await _userService.GetByLogin(login);

            if (user == null)
            {
                _logger.LogWarning($"Пользователь не найден {login}");
                return null;
            }

            return GetClaimsIdentity(user);
        }

        /// <summary>
        /// Получить идентификационные данные
        /// </summary>
        /// <param name="login">Логин</param>
        /// <param name="password">Пароль</param>
        /// <returns></returns>
        private async Task<ClaimsIdentity> GetIdentity(string login, string password)
        {
            // TODO: добавить шифрование пароля
            var hashPassword = password;
            var user = await _userService.GetByLogin(login);

            if (user == null)
            {
                _logger.LogWarning($"Пользователь не найден {login}");
                return null;
            }

            if (!user.VerifyPassword(hashPassword))
            {
                _logger.LogWarning($"Неверный пароль для {login}");
                return null;
            }

            return GetClaimsIdentity(user);
        }

        /// <summary>
        /// Получить идентификационные данные
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        private static ClaimsIdentity GetClaimsIdentity([NotNull] User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.FullName),
                new Claim("login", user.Login),
                new Claim("id", user.Id.ToString()),
            };

            var claimsIdentity = new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            return claimsIdentity;
        }
    }
}
