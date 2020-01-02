namespace Eventador.Domain.Types
{
    /// <summary>
    /// Уровень досупа к собитию
    /// </summary>
    public enum AccessType
    {
        /// <summary>
        /// Общедоступный
        /// </summary>
        PUBLIC = 0,

        /// <summary>
        /// Видно друзьям
        /// </summary>
        PRIVATE = 1,

        /// <summary>
        /// Только по приглашению
        /// </summary>
        INVITE = 2
    }
}