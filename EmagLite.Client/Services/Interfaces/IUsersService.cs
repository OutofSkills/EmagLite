using Models;
using System.Net.Http;
using System.Threading.Tasks;

namespace EmagLite.Client.Services.Interfaces
{
    public interface IUsersService
    {
        Task<HttpResponseMessage> GetUserAsync(int id);
    }
}
