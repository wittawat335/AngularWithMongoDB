using Frontend.Core.AppSettings;
using Frontend.Core.Interfaces;
using Frontend.DTOs;
using Frontend.Models.ViewModel.Product;
using Frontend.Utilities;
using System.Security.Policy;

namespace Frontend.Core.Services
{
    public class ProductService : IProductService
    {
        private readonly IBaseApiService<ProductDTO> _baseApiService;
        private readonly IBaseApiService<CategoryDTO> _cateApiService;
        private readonly IAppSetting _config;

        public ProductService(IBaseApiService<ProductDTO> baseApiService, IBaseApiService<CategoryDTO> cateApiService, IAppSetting config)
        {
            _baseApiService = baseApiService;
            _cateApiService = cateApiService;
            _config = config;
        }

        public async Task<ProductViewModel> Detail(string id, string action, string url, string url2)
        {
            var responseProduct = new Response<ProductDTO>();
            var responseCategory = new Response<List<CategoryDTO>>();
            var model = new ProductViewModel();
            var urlProductApi = _config.BaseUrlApi + url;
            var urlCategoryApi = _config.BaseUrlApi + url2;

            try
            {
                if (!string.IsNullOrEmpty(id))
                    responseProduct = await _baseApiService.GetAsyncById(urlProductApi, id);

                responseCategory = await _cateApiService.GetListAsync(urlCategoryApi);
                model.productDTO = responseProduct.Value;
                model.listCategory = responseCategory.Value;
                model.action = action;

            }
            catch
            {
                throw;
            }

            return model;
        }

        public async Task<ResponseStatus> Save(ProductViewModel model)
        {
            var response = new ResponseStatus();
            var baseUrlApi = _config.BaseUrlApi;
            try
            {
                if (model != null)
                {
                    switch (model.action)
                    {
                        case Constants.Action.New:
                            response = await _baseApiService.InsertAsync(model.productDTO, baseUrlApi + "Product/Add");
                            break;

                        case Constants.Action.Edit:
                            response = await _baseApiService.PutAsync(model.productDTO, baseUrlApi + "Product/Update");
                            break;

                        default:
                            response.Message = Constants.MessageError.CallAPI;
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }
            return response;
        }

        public Task<ProductViewModel> GetById(string id)
        {
            throw new NotImplementedException();
        }

        //public async Task<Response<List<ProductViewModel>>> GetList(string url)
        //{
        //    var urlApi = _config.BaseUrlApi + url;
        //    var response = new Response<List<ProductViewModel>>();
        //    try
        //    {
        //        response = await _baseApiService.GetListAsync(urlApi);

        //    }
        //    catch (Exception ex)
        //    {
        //        response.Message = ex.Message;
        //    }

        //    return response;
        //}
    }
}
