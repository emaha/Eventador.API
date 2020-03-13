using System.Threading.Tasks;
using Eventador.Common.Services;
using Eventador.Domain;

namespace Eventador.Services.Contract
{
    /// <summary>
    /// Интерфейс сервиса участия
    /// </summary>
    public interface IParticipationService : IBaseService<Participation>
    {
        /// <summary>
        /// Получить список участников по Id события
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Participation[]> GetByEventId(long id);

        /// <summary>
        /// Получить список участий пользователя по его Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Participation[]> GetByUserId(long id);
    }
}
