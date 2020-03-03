using Eventador.API.Common.Repositories;
using Eventador.API.Data.Contract;
using Eventador.API.Domain;

namespace Eventador.API.Data.Repositories
{
    public class EventRepository : BaseRepository<Event>, IEventRepository
    {
        public EventRepository(EventadorDbContext context) : base(context)
        {
        }
    }
}
