using Eventador.API.Common.Services;
using Eventador.API.Data.Contract;
using Eventador.API.Domain;
using Eventador.API.Services.Contract;
using Eventador.API.Common.Repositories;

namespace Eventador.API.Services
{
    public class EventService : BaseService<Event>, IEventService
    {
        public EventService(IEventRepository repository) : base(repository)
        {
        }
    }
}
