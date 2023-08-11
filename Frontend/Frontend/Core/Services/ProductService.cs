using Frontend.Core.AppSettings;
using Frontend.Core.Interfaces;
using Frontend.DTOs;
using Frontend.Models.ViewModel.Product;
using Frontend.Utilities;
using Newtonsoft.Json;
using System.IO;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;

namespace Frontend.Core.Services
{
    public class ProductService : IProductService
    {
        private readonly IBaseApiService<ProductDTO> _baseApiService;
        private readonly IBaseApiService<CategoryDTO> _cateApiService;
        private readonly IAppSetting _config;
        HttpClientHandler _httpClientHandler = new HttpClientHandler();
        Utilities.Common common = new Utilities.Common();
        public ProductService(IBaseApiService<ProductDTO> baseApiService, IBaseApiService<CategoryDTO> cateApiService, IAppSetting config)
        {
            _cateApiService = cateApiService;
            _config = config;
            _baseApiService = baseApiService;
            _httpClientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
        }
        public async Task<Response<List<ProductDTO>>> Search(string url, ProductSearch filter)
        {
            var session = common.GetValueBySession();
            var response = new Response<List<ProductDTO>>();
            try
            {
                using (var client = new HttpClient(_httpClientHandler))
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", session.AccessToken);
                    HttpResponseMessage result = await client.PostAsJsonAsync(url, filter);
                    if (result.IsSuccessStatusCode)
                    {
                        string data = result.Content.ReadAsStringAsync().Result;
                        response = JsonConvert.DeserializeObject<Response<List<ProductDTO>>>(data);
                    }
                    else
                        response.Message = Constants.MessageError.CallAPI;
                }
            }
            catch
            {
                throw;
            }
            return response;
        }
        public async Task<ProductViewModel> Detail(string id, string action)
        {
            var responseProduct = new Response<ProductDTO>();
            var model = new ProductViewModel();
            var urlProductApi = _config.BaseUrlApi + string.Format("Product/GetById?id={0}", id);
            var urlCategoryApi = _config.BaseUrlApi + "Category/GetAll";
            try
            {
                if (!string.IsNullOrEmpty(id))
                    responseProduct = await _baseApiService.GetAsyncById(urlProductApi);

                var listCategory = await _cateApiService.GetListAsync(urlCategoryApi);
                model.productDTO = responseProduct.Value;
                model.listCategory = listCategory.Value;
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
                            response = await _baseApiService.InsertAsync(baseUrlApi + "Product/Add", model.productDTO);
                            break;

                        case Constants.Action.Edit:
                            response = await _baseApiService.PutAsync(baseUrlApi + "Product/Update", model.productDTO);
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
        public async Task<List<ProductDTO>> Select2Product(string url, string query)
        {
            var filter = new ProductSearch();
            var response = await Search(url, filter);
            response.Value = response.Value.Where(x => x.ProductName.ToLower().Contains(query.ToLower())).ToList();

            return response.Value;
        }
    }
}
