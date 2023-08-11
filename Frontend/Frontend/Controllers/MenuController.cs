using Frontend.Core.AppSettings;
using Frontend.Core.Interfaces;
using Frontend.DTOs;
using Frontend.Models.ViewModel.Menu;
using Frontend.Models.ViewModel.Product;
using Microsoft.AspNetCore.Mvc;

namespace Frontend.Controllers
{
    public class MenuController : Controller
    {
        private readonly IBaseApiService<MenuDTO> _MenuApiService;
        private readonly IMenuService _service;
        private readonly IAppSetting _config;

        public MenuController(IBaseApiService<MenuDTO> menuApiService, IMenuService service, IAppSetting config)
        {
            _MenuApiService = menuApiService;
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

        [HttpPost]
        public async Task<IActionResult> Save(MenuViewModel model)
        {
            return new JsonResult(await _service.Save(model));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            return new JsonResult(await _MenuApiService.DeleteAsync(_config.BaseUrlApi + string.Format("Menu/{0}", id)));
        }
    }
}
