using Frontend.Models;
using Frontend.Models.ViewModel.Login;
using Frontend.Utilities;

namespace Frontend.Core.Interfaces
{
    public interface ILoginService
    {
        Task<Response<LoginVIewModel>> Login(LoginVIewModel model);
    }
}
