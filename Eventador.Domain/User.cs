using Eventador.Domain.Types;
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
        /// Дополнителная информация
        /// </summary>
        [Column("about_info")]
        public string AboutInfo { get; set; }
    }
}
