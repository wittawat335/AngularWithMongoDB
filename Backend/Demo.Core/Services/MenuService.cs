using AutoMapper;
using Demo.Core.Interfaces;
using Demo.Domain.DTOs.Menu;
using Demo.Domain.Models;
using Demo.Domain.Models.Collections;
using Demo.Domain.RepositoryContract;
using Demo.Domain.Utilities;
using Microsoft.AspNetCore.Identity;

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

        public Response<List<MenuDTO>> GetListMenuExists(string role)
        {
            var response = new Response<List<MenuDTO>>();
            IQueryable<RoleMenu> tbRoleMenu = _roleMenuRepository.AsQueryable();
            IQueryable<Menu> tbMenu = _menuRepository.AsQueryable();
            try
            {
                var siteLst = tbRoleMenu.Where(y => y.Role == role).Select(x => x.MenuCode);
                var _result = tbMenu.Where(x => !siteLst.Contains(x.MenuCode));

                response.Value = _mapper.Map<List<MenuDTO>>(_result);
                response.IsSuccess = Constants.StatusData.True;
                response.Message = Constants.Msg.GetList;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }
            return response;
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
            if (role != null)
                tbRoleMenu = tbRoleMenu.Where(x => x.Role == role);
            IQueryable<Menu> tbMenu = _menuRepository.AsQueryable();
            var response = new Response<List<RoleMenuDTO>>();
            try
            {
                IQueryable<RoleMenuDTO> tbResult = (from r in tbRoleMenu
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
        public async Task<Response<MenuDTO>> AddAsync(MenuInput model)
        {
            var response = new Response<MenuDTO>();
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
        public async Task<Response<MenuDTO>> UpdateAsync(MenuDTO model)
        {
            var response = new Response<MenuDTO>();
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
        public async Task<Response<MenuDTO>> DeleteByIdAsync(string id)
        {
            var response = new Response<MenuDTO>();
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

        public async Task<Response<RoleMenuDTO>> DeleteRoleMenuByIdAsync(string id)
        {
            var response = new Response<RoleMenuDTO>();
            try
            {
                await _roleMenuRepository.DeleteByIdAsync(id);
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
        public async Task<Response<RoleMenuDTO>> AddRoleManuAsync(RoleMenuDTO model)
        {
            var response = new Response<RoleMenuDTO>();
            try
            {
                var data = _mapper.Map<RoleMenuInput>(model);
                await _roleMenuRepository.InsertOneAsync(_mapper.Map<RoleMenu>(data));

                response.Value = model;
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
