using Frontend.Core.AppSettings;
using Frontend.Core.Interfaces;
using Frontend.DTOs;
using Frontend.Models.ViewModel.Product;
using Frontend.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Frontend.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _service;
        private readonly IBaseApiService<CategoryDTO> _cateApiService;
        private readonly IAppSetting _config;

        public ProductController(IProductService service, IBaseApiService<CategoryDTO> cateApiService, IAppSetting config)
        {
            _service = service;
            _cateApiService = cateApiService;
            _config = config;
        }

        public async Task<IActionResult> Index()
        {
            var model = new ProductViewModel();
            var listCategory = await _cateApiService.GetListAsync(_config.BaseUrlApi + "Category/GetAll");

            model.listCategory = listCategory.Value;

            return View(model);
        }

        public async Task<IActionResult> GetListAsync(ProductSearch search)
        {
            return new JsonResult(await _service.Search(_config.BaseUrlApi + "Product/GetAll", search));
        }

        public async Task<IActionResult> select2Product(string query)
        {
            return new JsonResult(await _service.Select2Product(_config.BaseUrlApi + "Product/GetAll", query));
        }

        [HttpPost]
        public async Task<IActionResult> _Detail(string id, string action)
        {
            return PartialView(await _service.Detail(id, action));
        }

        [HttpPost]
        public async Task<IActionResult> Save(ProductViewModel model)
        {
            return new JsonResult(await _service.Save(model));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string code)
        {
            return new JsonResult(await _service.Delete(code));
        }
    }
}
