namespace Eventador.Domain.Types
{
    /// <summary>
    /// Тип пользователя
    /// </summary>
    public enum UserType
    {
        /// <summary>
        /// Нопределён
        /// </summary>
        NONE = 0,

        /// <summary>
        /// Физ.лицо
        /// </summary>
        PERSON = 1,

        /// <summary>
        /// Организация
        /// </summary>
        ORGANIZATION = 2
    }
}