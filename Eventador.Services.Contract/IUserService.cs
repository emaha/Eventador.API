using System.Threading.Tasks;
using Eventador.Common.Services;
using Eventador.Domain;

namespace Eventador.Services.Contract
{
    /// <summary>
    ///
    /// </summary>
    public interface IUserService : IBaseService<User>
    {
        /// <summary>
        /// Получить пользователя по логину
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        Task<User> GetByLogin(string login);
    }
}
