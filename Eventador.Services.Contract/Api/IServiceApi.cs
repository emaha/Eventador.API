using System.Threading.Tasks;
using Refit;

namespace Eventador.Services.Contract.Api
{
    public interface IServiceApi
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        [Put("/Foo/Bar")]
        Task FakeMethod(long id);
    }
}
