using Eventador.Common.Repositories;
using Eventador.Common.Services;
using Eventador.Data.Contract;
using Eventador.Domain;
using Eventador.Services.Contract;

namespace Eventador.Services
{
    public class EventService : BaseService<Event>, IEventService
    {
        public EventService(IEventRepository repository) : base(repository)
        {
        }
    }
}
