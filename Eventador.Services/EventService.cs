using Eventador.Common.Services;
using Eventador.Data.Contract;
using Eventador.Domain;
using Eventador.Services.Contract;
using System.Linq;
using System.Threading.Tasks;

namespace Eventador.Services
{
    public class EventService : BaseService<Event>, IEventService
    {
        private readonly IEventRepository _repository;

        public EventService(IEventRepository repository) : base(repository)
        {
            _repository = repository;
        }

        public async Task<Event[]> GetByRegion(long regionId)
        {
            return await _repository.GetAll(x =>
                x.RegionId == regionId &&
                x.Status == Domain.Statuses.EventStatus.Active
            );
        }

        public async Task<Event[]> GetByAuthorId(long id)
        {
            return await _repository.GetAll(x => x.AuthorId == id);
        }

        public async Task<Event[]> GetByIds(long[] ids)
        {
            return await _repository.GetAll(x => ids.Contains(x.Id));
        }

        public async Task<Event[]> GetBySearchRequest(string requset)
        {
            // TODO: сделать запрос
            return await _repository.GetAll(x => x.Title.Contains(requset));
        }
    }
}
