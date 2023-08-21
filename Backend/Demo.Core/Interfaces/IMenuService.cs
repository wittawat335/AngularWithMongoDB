using Demo.Domain.DTOs.Menu;
using Demo.Domain.DTOs.Product;
using Demo.Domain.Models;
using Demo.Domain.Models.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Core.Interfaces
{
    public interface IMenuService
    {
        Response<List<MenuDTO>> GetListMenuExists(string role);
        Task<Response<string>> GetMenuNameByCode(string code);
        Response<List<MenuDTO>> GetList(Guid userId);
        Task<Response<List<MenuDTO>>> GetAll();
        Task<Response<List<RoleMenuDTO>>> GetAllRoleMenu(string role);
        Task<Response<MenuDTO>> GetByIdAsync(string id);
        Task<Response<MenuDTO>> AddAsync(MenuInput model);
        Task<Response<RoleMenuDTO>> AddRoleManuAsync(RoleMenuDTO model);
        Task<Response<MenuDTO>> UpdateAsync(MenuDTO model);
        Task<Response<MenuDTO>> DeleteByIdAsync(string id);
        Task<Response<RoleMenuDTO>> DeleteRoleMenuByIdAsync(string id);
    }
}
