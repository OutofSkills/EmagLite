using Models.ViewModels;
using System.Threading.Tasks;

namespace RESTApi.Services.Intefaces
{
    public interface IAccountService
    {
        Task<string> LoginAsync(LoginRequest loginRequest);
        Task RegisterAsync(RegisterRequest registerRequest);
    }
}