using Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace EmagLite.Client.Services.Interfaces
{
    public interface IUsersService
    {
        Task<User> GetUserAsync(int id);
        Task<IEnumerable<User>> GetUsersAsync();
        Task RemoveUserAsync(int id);
    }
}
