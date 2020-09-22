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
        None = 0,

        /// <summary>
        /// Физ.лицо
        /// </summary>
        Person = 1,

        /// <summary>
        /// Организация
        /// </summary>
        Organization = 2
    }
}
