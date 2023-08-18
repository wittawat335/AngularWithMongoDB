using Frontend.Core.AppSettings;
using Frontend.Core.Interfaces;
using Frontend.DTOs;
using Frontend.Models.ViewModel.Menu;
using Frontend.Models.ViewModel.Product;
using Frontend.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace Frontend.Controllers
{
    public class MenuController : Controller
    {
        private readonly IBaseApiService<MenuDTO> _MenuApiService;
        private readonly IBaseApiService<RoleDTO> _roleApiService;
        private readonly IBaseApiService<RoleMenuDTO> _roleMenuApiService;
        private readonly IMenuService _service;
        private readonly IAppSetting _config;

        public MenuController(
            IBaseApiService<MenuDTO> menuApiService,
            IBaseApiService<RoleDTO> roleApiService,
            IBaseApiService<RoleMenuDTO> roleMenuApiService,
            IMenuService service,
            IAppSetting config)
        {
            _MenuApiService = menuApiService;
            _roleApiService = roleApiService;
            _roleMenuApiService = roleMenuApiService;
            _service = service;
            _config = config;
        }

        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> GetListAsync()
        {
            return new JsonResult(await _MenuApiService.GetListAsync(_config.BaseUrlApi + "Menu/GetAll"));
        }

        [HttpPost]
        public async Task<IActionResult> _Detail(string id, string action)
        {
            return PartialView(await _service.Detail(id, action));
        }

        public async Task<IActionResult> GetListRoleMenu()
        {
            return new JsonResult(await _roleMenuApiService.GetListAsync(_config.BaseUrlApi + "Menu/GetListRoleMenu"));
        }

        [HttpPost]
        public async Task<IActionResult> GetListRoleMenuByRole(string role)
        {
            var list = await _roleMenuApiService.GetListAsync(_config.BaseUrlApi + string.Format("Menu/GetListRoleMenu?role={0}", role));
            return PartialView("_ShowListRoleMenu", list.Value);
        }

        [HttpPost]
        public async Task<IActionResult> _RoleMenu() //Modal Lv1
        {
            var model = new MenuViewModel();
            var listRole = await _roleApiService.GetListAsync(_config.BaseUrlApi + "Authentication/GetRoleList");

            model.listRole = listRole.Value;

            return PartialView(model);
        }

        [HttpPost]
        public async Task<IActionResult> _AddRoleMenu() //Modal Lv2
        {
            var model = new MenuViewModel();
            var listRole = await _roleApiService.GetListAsync(_config.BaseUrlApi + "Authentication/GetRoleList");
            var listMenu = await _MenuApiService.GetListAsync(_config.BaseUrlApi + "Menu/GetAll");

            model.listRole = listRole.Value;
            model.listMenu = listMenu.Value;

            return PartialView(model);
        }

        [HttpPost]
        public async Task<IActionResult> Save(MenuViewModel model)
        {
            return new JsonResult(await _service.Save(model));
        }

        [HttpPost]
        public async Task<IActionResult> SaveRoleMenu(MenuViewModel model)
        {
            return new JsonResult(await _roleMenuApiService.InsertAsync(_config.BaseUrlApi + "Menu/AddRoleMenu", model.roleMenuDTO));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            return new JsonResult(await _MenuApiService.DeleteAsync(_config.BaseUrlApi + string.Format("Menu/{0}", id)));
        }

        [HttpPost]
        public async Task<IActionResult> DeleteRoleMenu(string id)
        {
            return new JsonResult(await _MenuApiService.DeleteAsync(_config.BaseUrlApi + string.Format("Menu/DeleteRoleMenu/{0}", id)));
        }
    }
}
