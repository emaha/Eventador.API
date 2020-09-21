using Eventador.Domain;
using System;

namespace Eventador.Services.Contract.Api
{
    public class UserResponseModel
    {
        /// <summary>
        /// Id
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Фамилия
        /// </summary>
        public string Lastname { get; set; }

        /// <summary>
        /// Имя
        /// </summary>
        public string Firstname { get; set; }

        /// <summary>
        /// Отчетсво
        /// </summary>
        public string Middlename { get; set; }

        /// <summary>
        /// Контактный телефон
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// Рейтинг пользователя (расчетный)
        /// </summary>
        public double Rating { get; set; }

        /// <summary>
        /// О себе
        /// </summary>
        public string AboutInfo { get; set; }

        public static UserResponseModel Create(User user)
        {
            return new UserResponseModel
            {
                Id = user.Id,
                Lastname = user.LastName,
                Firstname = user.Firstname,
                Middlename = user.MiddleName,
                Phone = user.Phone,
                Rating = new Random().NextDouble() * 5f,
                AboutInfo = user.AboutInfo
            };
        }
    }
}
