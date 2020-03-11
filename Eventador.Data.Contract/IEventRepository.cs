using Eventador.Common.Repositories;
using Eventador.Domain;

namespace Eventador.Data.Contract
{
    /// <summary>
    /// Интерфейс сущности события
    /// </summary>
    public interface IEventRepository : IBaseRepository<Event>
    {
    }
}
