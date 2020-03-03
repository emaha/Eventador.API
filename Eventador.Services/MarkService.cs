using System.Threading.Tasks;
using Eventador.Common.Services;
using Eventador.Data.Contract;
using Eventador.Domain;
using Eventador.Services.Contract;

namespace Eventador.Services
{
    public class MarkService : BaseService<Mark>, IMarkService
    {
        private readonly IMarkRepository _repository;

        public MarkService(IMarkRepository repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
