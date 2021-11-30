using EmagLite.Client.Services.Interfaces;
using System.Net.Http;
using System.Threading.Tasks;

namespace EmagLite.Client.Services.Implementations
{
    public class UsersService : IUsersService
    {
        private readonly HttpClient httpClient;

        public UsersService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<HttpResponseMessage> GetUserAsync(int id)
        {
            return await httpClient.GetAsync($"/api/Users/{id}");
        }
    }
}
