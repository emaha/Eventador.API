using System.ComponentModel.DataAnnotations;

namespace Eventador.API.Requests
{
    /// <summary>
    /// Запрос на создание Refresh-токена
    /// </summary>
    public class RefreshTokenRequest
    {
        /// <summary>
        /// Токен для обновления
        /// </summary>
        [Required]
        public string RefreshToken { get; set; }
    }
}
