using System.ComponentModel.DataAnnotations;

namespace Eventador.API.Requests
{
    /// <summary>
    /// Данные авторизации
    /// </summary>
    public class CredentialsRequest
    {
        /// <summary>
        /// Логин
        /// </summary>
        [Required]
        public string Username { get; set; }

        /// <summary>
        /// Пароль
        /// </summary>
        [Required]
        public string Password { get; set; }
    }
}
