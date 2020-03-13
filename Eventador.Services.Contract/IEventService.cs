using System.Threading.Tasks;
using Eventador.Common.Services;
using Eventador.Domain;

namespace Eventador.Services.Contract
{
    /// <summary>
    ///
    /// </summary>
    public interface IEventService : IBaseService<Event>
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="regionId"></param>
        /// <returns></returns>
        Task<Event[]> GetByRegion(long regionId);

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Event[]> GetByAuthorId(long id);

        /// <summary>
        /// Получить несколько событий по Id
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        Task<Event[]> GetByIds(long[] ids);
    }
}
