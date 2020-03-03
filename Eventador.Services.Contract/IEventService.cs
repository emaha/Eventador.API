using System.Threading.Tasks;
using Eventador.Common.Services;
using Eventador.Domain;

namespace Eventador.Services.Contract
{
    public interface IEventService : IBaseService<Event>
    {
        Task<Event[]> GetByRegion(int regionId);
    }
}
