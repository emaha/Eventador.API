using Eventador.Common.Repositories;
using Eventador.Data.Contract;
using Eventador.Domain;

namespace Eventador.Data.Repositories
{
    /// <summary>
    /// Репозиторий события
    /// </summary>
    public class EventRepository : BaseRepository<Event>, IEventRepository
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="context"></param>
        public EventRepository(EventadorDbContext context) : base(context)
        {
        }
    }
}
