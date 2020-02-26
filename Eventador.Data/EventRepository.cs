using Eventador.Common.Repositories;
using Eventador.Data.Contract;
using Eventador.Domain;

namespace Eventador.Data
{
    public class EventRepository : BaseRepository<Event>, IEventRepository
    {
        public EventRepository(EventadorDbContext context) : base(context)
        {
        }
    }
}
