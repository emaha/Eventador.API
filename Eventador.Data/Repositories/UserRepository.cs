using Eventador.Common.Repositories;
using Eventador.Data.Contract;
using Eventador.Domain;

namespace Eventador.Data.Repositories
{
    /// <summary>
    /// Репозиторий пользователя
    /// </summary>
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="context"></param>
        public UserRepository(EventadorDbContext context) : base(context)
        {
        }
    }
}
