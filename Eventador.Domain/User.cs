﻿using Eventador.Domain.Types;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eventador.Domain
{
    /// <summary>
    /// Пользователь (карточка пользователя)
    /// </summary>
    [Table("users")]
    public class User
    {
        /// <summary>
        /// ID
        /// </summary>
        [Column("id")]
        public long Id { get; set; }

        /// <summary>
        /// Фамилия
        /// </summary>
        [Column("last_name")]
        public string LastName { get; set; }

        /// <summary>
        /// Имя
        /// </summary>
        [Column("first_name")]
        public string Firstname { get; set; }

        /// <summary>
        /// Отчество
        /// </summary>
        [Column("middle_name")]
        public string MiddleName { get; set; }

        /// <summary>
        /// Логин
        /// </summary>
        [Column("login")]
        public string Login { get; set; }

        /// <summary>
        /// Пароль
        /// </summary>
        [Column("password")]
        public string Password { get; set; }

        /// <summary>
        /// Флаг блокировки
        /// </summary>
        [Column("blocked")]
        public bool Blocked { get; set; }

        /// <summary>
        /// Тип пользователя
        /// </summary>
        [Column("user_type")]
        public UserType UserType { get; set; }

        /// <summary>
        /// Наименование организации (если есть)
        /// </summary>
        [Column("organization_name")]
        public string OrganizationName { get; set; }

        /// <summary>
        /// Контактный телефон
        /// </summary>
        [Column("phone")]
        public string Phone { get; set; }

        /// <summary>
        /// Дополнительная информация
        /// </summary>
        [Column("about_info")]
        public string AboutInfo { get; set; }

        /// <summary>
        /// ФИО
        /// </summary>
        public string FullName => $"{LastName} {Firstname} {MiddleName}";

        /// <summary>
        /// Сверка пароля
        /// </summary>
        /// <param name="hashPassword"></param>
        /// <returns></returns>
        public bool VerifyPassword(string hashPassword)
        {
            return Password == hashPassword;
        }
    }
}
