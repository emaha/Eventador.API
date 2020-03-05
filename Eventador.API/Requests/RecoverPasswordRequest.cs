using System.ComponentModel.DataAnnotations;

namespace Eventador.API.Requests
{
    /// <summary>
    /// Запрос на восстановление пароля
    /// </summary>
    public class RecoverPasswordRequest
    {
        /// <summary>
        /// Логин пользователя. Номер телефона
        /// </summary>
        [Required]
        public string Username { get; set; }
    }
}
