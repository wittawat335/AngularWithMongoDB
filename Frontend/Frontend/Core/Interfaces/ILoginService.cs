using Frontend.Models;
using Frontend.Models.ViewModel.Login;
using Frontend.Utilities;

namespace Frontend.Core.Interfaces
{
    public interface ILoginService
    {
        Task<Response<LoginResponse>> Login(LoginRequest model);
    }
}
