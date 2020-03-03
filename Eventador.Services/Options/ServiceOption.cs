using System;
using System.Collections.Generic;
using System.Text;

namespace Eventador.API.Services.Options
{
    public class ServiceOption
    {
        /// <summary>
        /// Адрес сервера
        /// </summary>
        public string Server { get; set; }

        /// <summary>
        /// Таймаут в секундах
        /// </summary>
        public int TimeoutSec { get; set; }

        /// <summary>
        /// Токен авторизации
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// Схема
        /// </summary>
        public string Scheme { get; set; }
    }
}
