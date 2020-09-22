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
        None = 0,

        /// <summary>
        /// Активен
        /// </summary>
        Active = 1,

        /// <summary>
        /// Приостановлено
        /// </summary>
        Suspended = 2,

        /// <summary>
        /// Завершен
        /// </summary>
        Finished = 3,

        /// <summary>
        /// Отменено
        /// </summary>
        Canceled = 4
    }
}
