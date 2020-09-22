namespace Eventador.Domain.Types
{
    /// <summary>
    /// Уровень доступа к событию
    /// </summary>
    public enum AccessType
    {
        /// <summary>
        /// Общедоступный
        /// </summary>
        Public = 0,

        /// <summary>
        /// Видно друзьям
        /// </summary>
        Private = 1,

        /// <summary>
        /// Только по приглашению
        /// </summary>
        Invite = 2
    }
}
