using Demo.Domain.DTOs.User;
using Demo.Domain.Models;

namespace Demo.Core.Interfaces
{
    public interface IAuthenticateService
    {
        Task<Response<LoginResponse>> LoginAsync(LoginRequest request);
        Task<ResponseStatus> RegisterAsync(RegisterRequest request);
        Task<ResponseStatus> CreateRoleAsync(CreateRoleRequest request);
    }
}
