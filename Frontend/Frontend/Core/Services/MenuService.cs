using Frontend.Core.AppSettings;
using Frontend.Core.Interfaces;
using Frontend.DTOs;
using Frontend.Models.ViewModel.Menu;
using Frontend.Utilities;

namespace Frontend.Core.Services
{
    public class MenuService : IMenuService
    {
        private readonly IBaseApiService<MenuDTO> _baseApiService;
        private readonly IAppSetting _config;
        HttpClientHandler _httpClientHandler = new HttpClientHandler();
        Common common = new Common();

        public MenuService(IBaseApiService<MenuDTO> baseApiService, IAppSetting config)
        {
            _baseApiService = baseApiService;
            _config = config;
            _httpClientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
        }

        public async Task<MenuViewModel> Detail(string id, string action)
        {
            var responseProduct = new Response<MenuDTO>();
            var model = new MenuViewModel();
            var urlApi = _config.BaseUrlApi + string.Format("Menu/GetById?id={0}", id);
            try
            {
                if (!string.IsNullOrEmpty(id))
                    responseProduct = await _baseApiService.GetAsyncById(urlApi);

                model.menuDTO = responseProduct.Value;
                model.action = action;
            }
            catch
            {
                throw;
            }

            return model;
        }

        public async Task<ResponseStatus> Save(MenuViewModel model)
        {
            var response = new ResponseStatus();
            try
            {
                if (model != null)
                {
                    switch (model.action)
                    {
                        case Constants.Action.New:
                            response = await _baseApiService.InsertAsync(_config.BaseUrlApi + "Menu/Add", model.menuDTO);
                            break;

                        case Constants.Action.Edit:
                            response = await _baseApiService.PutAsync(_config.BaseUrlApi + "Menu/Update", model.menuDTO);
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
    }
}
