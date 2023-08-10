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
        private readonly IAppSetting _config;

        public ProductController(IProductService service, IBaseApiService<ProductDTO> baseApiService, IAppSetting config)
        {
            _service = service;
            _baseApiService = baseApiService;
            _config = config;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetListAsync()
        {

            return new JsonResult(await _baseApiService.GetListAsync(_config.BaseUrlApi + "Product/GetAll"));
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

        //[HttpPost]
        //public async Task<IActionResult> Delete(string code)
        //{
        //    return new JsonResult(await _service.Delete(code));
        //}
    }
}
