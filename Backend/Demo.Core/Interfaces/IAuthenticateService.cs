using Demo.Domain.DTOs.User;
using Demo.Domain.Models;

namespace Demo.Core.Interfaces
{
    public interface IAuthenticateService
    {
        Response<List<UserDTO>> GetList();
        Response<List<RoleDTO>> GetRoleList();
        Task<Response<UserDTO>> GetByIdAsync(string id);
        Task<Response<LoginResponse>> LoginAsync(LoginRequest request);
        Task<ResponseStatus> RegisterAsync(RegisterRequest request);
        Task<ResponseStatus> CreateRoleAsync(CreateRoleRequest request);
        Task<ResponseStatus> UpdateUser(UserDTO model);
        Task<ResponseStatus> DeleteUser(string id);
    }
}
