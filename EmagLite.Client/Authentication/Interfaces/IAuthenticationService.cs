using Models.ViewModels;
using System.Threading.Tasks;

namespace EmagLite.Client.Authentication.Interfaces
{
    public interface IAuthenticationService
    {
        Task<string> Register(RegisterRequest model);
        Task<bool> ChangePasswordAsync(ResetPasswordModel model, int id);
        Task<LoginResponse> Login(LoginRequest userForAtuthetication);
        Task Logout();
    }
}