using Tasahil_.net_core_6_.Models;

namespace Tasahil_.net_core_6_.Services
{
    public interface IAuthService
    {
        Task<AuthResponseVM> RegisterAsync(RegisterVM model);
        Task<AuthResponseVM> LoginAsync(LoginVM model);

        Task<AuthResponseVM> ForgetPass(ForgetPassVM model);
        Task<AuthResponseVM> resstPass(RessetPassVM model);
    }
}
