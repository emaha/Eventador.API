using System.Security.Claims;
using Eventador.Domain.Models;
using JetBrains.Annotations;

namespace Eventador.API.Services
{
    /// <summary>
    /// Интерфейс сервис для работы с авторизационным токеном JWT
    /// </summary>
    public interface ITokenService
    {
        /// <summary>
        /// Получить токен
        /// </summary>
        /// <param name="claimsIdentity">Идентификационные данные</param>
        /// <returns>Токен</returns>
        [NotNull]
        TokenModel GetToken([NotNull] ClaimsIdentity claimsIdentity);
    }
}
