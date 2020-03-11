namespace Eventador.Domain.Statuses
{
    /// <summary>
    /// Статусы событий
    /// </summary>
    public enum EventStatus
    {
        /// <summary>
        /// Без статуса
        /// </summary>
        NONE = 0,

        /// <summary>
        /// Активен
        /// </summary>
        ACTIVE = 1,

        /// <summary>
        /// Приостановлено
        /// </summary>
        SUSPENDED = 2,

        /// <summary>
        /// Завершен
        /// </summary>
        FINISHED = 3,

        /// <summary>
        /// Отменено
        /// </summary>
        CANCELED = 4
    }
}
