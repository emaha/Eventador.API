using Eventador.Common.Repositories;
using Eventador.Domain;

namespace Eventador.Data.Contract
{
    /// <summary>
    /// Интерфейс сущности пользователя
    /// </summary>
    public interface IUserRepository : IBaseRepository<User>
    {
    }
}
