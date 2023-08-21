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
        Task<Response<UserDTO>> RegisterAsync(RegisterRequest request);
        Task<Response<RoleDTO>> CreateRoleAsync(CreateRoleRequest request);
        Task<Response<UserDTO>> UpdateUser(UserDTO model);
        Task<Response<UserDTO>> DeleteUser(string id);
    }
}
