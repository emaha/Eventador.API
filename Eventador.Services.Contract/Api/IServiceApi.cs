using Refit;
using System.Threading.Tasks;

namespace Eventador.API.Services.Contract.Api
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
