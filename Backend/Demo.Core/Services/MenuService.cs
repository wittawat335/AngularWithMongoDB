using AutoMapper;
using Demo.Core.Interfaces;
using Demo.Domain.DTOs.Category;
using Demo.Domain.DTOs.Menu;
using Demo.Domain.DTOs.Product;
using Demo.Domain.Models;
using Demo.Domain.Models.Collections;
using Demo.Domain.RepositoryContract;
using Demo.Domain.Utilities;
using Microsoft.AspNetCore.Identity;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Demo.Core.Services
{
    public class MenuService : IMenuService
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly IMongoRepository<RoleMenu> _roleMenuRepository;
        private readonly IMongoRepository<Menu> _menuRepository;
        private readonly IMapper _mapper;

        public MenuService(
            UserManager<User> userManager,
            RoleManager<Role> roleManager,
            IMongoRepository<RoleMenu> roleMenuRepository,
            IMongoRepository<Menu> menuRepository,
            IMapper mapper)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _roleMenuRepository = roleMenuRepository;
            _menuRepository = menuRepository;
            _mapper = mapper;
        }
        public async Task<Response<List<MenuDTO>>> GetAll()
        {
            var response = new Response<List<MenuDTO>>();
            try
            {
                response.Value = _mapper.Map<List<MenuDTO>>(await _menuRepository.GetAll());
                response.IsSuccess = Constants.StatusData.True;
                response.Message = Constants.Msg.GetList;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return response;
        }
        public async Task<Response<List<RoleMenuDTO>>> GetAllRoleMenu(string role)
        {
            IQueryable<RoleMenu> tbRoleMenu = _roleMenuRepository.AsQueryable();
            IQueryable<Menu> tbMenu = _menuRepository.AsQueryable();
            var response = new Response<List<RoleMenuDTO>>();
            try
            {
                IQueryable<RoleMenuDTO> tbResult = (from r in tbRoleMenu.Where(x => x.Role == role)
                                                    join m in tbMenu on r.MenuCode equals m.MenuCode
                                                    select new RoleMenuDTO
                                                    {
                                                        Id = r.Id.ToString(),
                                                        Role = r.Role,
                                                        MenuCode = r.MenuCode,
                                                        MenuName = m.Name
                                                    }).AsQueryable();

                response.Value = tbResult.ToList();
                response.IsSuccess = Constants.StatusData.True;
                response.Message = Constants.Msg.GetList;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return response;
        }
        public async Task<Response<MenuDTO>> GetByIdAsync(string id)
        {
            var response = new Response<MenuDTO>();
            try
            {
                response.Value = _mapper.Map<MenuDTO>(await _menuRepository.FindByIdAsync(id));
                response.IsSuccess = Constants.StatusData.True;
                response.Message = Constants.Msg.UpdateComplete;

            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Exception Occurs : " + ex.Message;
            }

            return response;
        }
        public async Task<ResponseStatus> AddAsync(MenuInput model)
        {
            var response = new ResponseStatus();
            try
            {
                await _menuRepository.InsertOneAsync(_mapper.Map<Menu>(model));
                response.IsSuccess = Constants.StatusData.True;
                response.Message = Constants.Msg.InsertComplete;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Exception Occurs : " + ex.Message;
            }

            return response;
        }
        public async Task<ResponseStatus> UpdateAsync(MenuDTO model)
        {
            var response = new ResponseStatus();
            try
            {
                var findId = await _menuRepository.FindByIdAsync(model.Id);

                await _menuRepository.ReplaceOneAsync(_mapper.Map(model, findId));
                response.IsSuccess = Constants.StatusData.True;
                response.Message = Constants.Msg.UpdateComplete;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Exception Occurs : " + ex.Message;
            }

            return response;
        }
        public async Task<ResponseStatus> DeleteByIdAsync(string id)
        {
            var response = new ResponseStatus();
            try
            {
                await _menuRepository.DeleteByIdAsync(id);
                response.IsSuccess = Constants.StatusData.True;
                response.Message = Constants.Msg.DeleteComplete;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Exception Occurs : " + ex.Message;
            }

            return response;
        }
        public Response<List<MenuDTO>> GetList(Guid userId)
        {
            IQueryable<User> tbUser = _userManager.Users.Where(x => x.Id == userId);
            IQueryable<Role> tbRole = _roleManager.Roles;
            IQueryable<RoleMenu> tbRoleMenu = _roleMenuRepository.AsQueryable();
            IQueryable<Menu> tbMenu = _menuRepository.AsQueryable();
            var response = new Response<List<MenuDTO>>();
            try
            {

                IQueryable<Menu> tbResult = (from u in tbUser
                                             join rm in tbRoleMenu on u.Role equals rm.Role
                                             join m in tbMenu on rm.MenuCode equals m.MenuCode
                                             select m).AsQueryable();

                var listMenus = tbResult.ToList();
                response.Value = _mapper.Map<List<MenuDTO>>(listMenus);
                response.IsSuccess = true;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return response;
        }
        public async Task<ResponseStatus> AddRoleManuAsync(RoleMenuDTO model)
        {
            var response = new ResponseStatus();
            try
            {
                var data = _mapper.Map<RoleMenuInput>(model);
                await _roleMenuRepository.InsertOneAsync(_mapper.Map<RoleMenu>(data));
                response.IsSuccess = Constants.StatusData.True;
                response.Message = Constants.Msg.InsertComplete;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Exception Occurs : " + ex.Message;
            }

            return response;
        }
        public async Task<Response<string>> GetMenuNameByCode(string code)
        {
            var response = new Response<string>();
            try
            {
                var value = await _menuRepository.FindOneAsync(x => x.MenuCode == code);
                response.Value = value.Name;
                response.IsSuccess = Constants.StatusData.True;
                response.Message = Constants.Msg.InsertComplete;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Exception Occurs : " + ex.Message;
            }

            return response;
        }
    }
}
