using System.Threading.Tasks;
using Eventador.Common.Services;
using Eventador.Data.Contract;
using Eventador.Domain;
using Eventador.Services.Contract;

namespace Eventador.Services
{
    public class ParticipationService : BaseService<Participation>, IParticipationService
    {
        private readonly IParticipationRepository _repository;

        public ParticipationService(IParticipationRepository repository) : base(repository)
        {
            _repository = repository;
        }

        public async Task<Participation[]> GetByEventId(long id)
        {
            return await _repository.GetAll(x => x.EventId == id);
        }
    }
}
