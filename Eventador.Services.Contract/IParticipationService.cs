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
        Task<Participation[]> GetByEventId(long id);
    }
}
