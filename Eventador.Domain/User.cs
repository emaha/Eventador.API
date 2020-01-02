using Eventador.Domain.Types;

namespace Eventador.Domain
{
    /// <summary>
    /// Пользователь (карточка пользоавтеля)
    /// </summary>
    public class User
    {
        /// <summary>
        /// ID
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
        /// Отчество
        /// </summary>
        public string Middlename { get; set; }

        /// <summary>
        /// Тип польщователя
        /// </summary>
        public UserType UserType { get; set; }

        /// <summary>
        /// Наименование организации (если есть)
        /// </summary>
        public string OrganizationName { get; set; }
        
        /// <summary>
        /// Контанктный телефон
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// Дополнителная информация
        /// </summary>
        public string AboutInfo { get; set; }

    }
}