using Eventador.Common.Repositories;
using Eventador.Data.Contract;
using Eventador.Domain;

namespace Eventador.Data.Repositories
{
    /// <summary>
    /// Репозиторий события
    /// </summary>
    public class ParticipationRepository : BaseRepository<Participation>, IParticipationRepository
    {
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="context"></param>
        public ParticipationRepository(EventadorDbContext context) : base(context)
        {
        }
    }
}
