using System.Security.Claims;
using Eventador.Domain.Models;
using JetBrains.Annotations;

namespace Eventador.API.Services
{
    /// <summary>
    /// Интерфейс сервис для работы с авторизационным JWT с обновлением авторизационного токена
    /// </summary>
    public interface ITokenWithRefreshService : ITokenService
    {
        /// <summary>
        /// Получить токен по refresh token
        /// </summary>
        /// <param name="claimsIdentity">Идентификационные данные</param>
        /// <param name="refreshToken">Токен для обновления access token</param>
        /// <returns>Токен</returns>
        TokenModel GetTokenByRefreshToken([NotNull] ClaimsIdentity claimsIdentity, [NotNull] string refreshToken);

        /// <summary>
        /// Проверить refresh токен для пользователя
        /// </summary>
        /// <param name="login">Логин пользователя</param>
        /// <param name="refreshTokenForCheck">refresh токен для проверки</param>
        /// <returns>true-валидный,false - невалидный</returns>
        bool ValidRefreshToken([NotNull] string login, [NotNull] string refreshTokenForCheck);

        /// <summary>
        /// Удалить все refresh токен
        /// </summary>
        /// <param name="login">Логин пользователя</param>
        void DeleteAllRefreshToken([NotNull] string login);
    }
}
