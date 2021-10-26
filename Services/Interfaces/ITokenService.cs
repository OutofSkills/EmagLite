using Models.ViewModels;
using System.Threading.Tasks;

namespace RESTApi.Services.Intefaces
{
    public interface ITokenService
    {
        Task<string> GetTokenAsync(LoginRequest loginRequest);
        Task CreateTokenAsync(RegisterRequest registerRequest);
    }
}