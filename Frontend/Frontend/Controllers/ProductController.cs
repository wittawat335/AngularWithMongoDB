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
        private readonly IBaseApiService<ProductDTO> _baseApiService;
        private readonly IBaseApiService<CategoryDTO> _cateApiService;
        private readonly IAppSetting _config;

        public ProductController(IProductService service,
            IBaseApiService<ProductDTO> baseApiService,
            IBaseApiService<CategoryDTO> cateApiService,
            IAppSetting config)
        {
            _service = service;
            _baseApiService = baseApiService;
            _cateApiService = cateApiService;
            _config = config;
        }

        public async Task<IActionResult> Index()
        {
            var response = new Response<List<CategoryDTO>>();
            var model = new ProductViewModel();
            response = await _cateApiService.GetListAsync(_config.BaseUrlApi + "Category/GetAll");
            model.listCategory = response.Value;

            return View(model);
        }

        public async Task<IActionResult> GetListAsync(ProductSearch search)
        {
            var response = new Response<List<ProductDTO>>();
            response = await _service.Search(_config.BaseUrlApi + "Product/Search", search);

            return new JsonResult(response);
            //return new JsonResult(await _baseApiService.GetListAsync(_config.BaseUrlApi + "Product/GetAll"));
        }

        [HttpPost]
        public async Task<IActionResult> Search(ProductSearch search)
        {
            return new JsonResult(await _service.Search(_config.BaseUrlApi + "Product/GetAll", search));
        }

        [HttpPost]
        public async Task<IActionResult> _Detail(string id, string action)
        {
            return PartialView(await _service.Detail(id, action, "Product", "Category/GetAll"));
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
