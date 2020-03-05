namespace Eventador.Domain.Models
{
    /// <summary>
    /// Модель получения токенов
    /// </summary>
    public class TokenModel
    {
        /// <summary>
        /// Токен доступа
        /// </summary>
        public string AccessToken { get; set; }

        /// <summary>
        /// Токен для обновления
        /// </summary>
        public string RefreshToken { get; set; }

        /// <summary>
        /// Время истечения токена доступа
        /// </summary>
        public long Expires { get; set; }
    }
}
