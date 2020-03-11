using Eventador.Common.Repositories;
using Eventador.Data.Contract;
using Eventador.Domain;

namespace Eventador.Data.Repositories
{
    /// <summary>
    /// Репозиторий отзыва
    /// </summary>
    public class MarkRepository : BaseRepository<Mark>, IMarkRepository
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="context"></param>
        public MarkRepository(EventadorDbContext context) : base(context)
        {
        }
    }
}
