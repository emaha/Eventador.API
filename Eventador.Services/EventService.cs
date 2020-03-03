using System.Threading.Tasks;
using Eventador.Common.Services;
using Eventador.Data.Contract;
using Eventador.Domain;
using Eventador.Services.Contract;

namespace Eventador.Services
{
    public class EventService : BaseService<Event>, IEventService
    {
        private readonly IEventRepository _repository;

        public EventService(IEventRepository repository) : base(repository)
        {
            _repository = repository;
        }

        public async Task<Event[]> GetByRegion(int regionId)
        {
            return await _repository.GetAll(x => x.RegionId == regionId);
        }
    }
}
