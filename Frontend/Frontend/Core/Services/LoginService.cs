using Frontend.Core.AppSettings;
using Frontend.Core.Interfaces;
using Frontend.Models.ViewModel.Login;
using Frontend.Utilities;
using Newtonsoft.Json;

namespace Frontend.Core.Services
{
    public class LoginService : ILoginService
    {
        private readonly IAppSetting _config;
        private readonly IBaseApiService<LoginVIewModel> _baseApiService;
        private readonly IHttpContextAccessor _contextAccessor;

        public LoginService(
            IAppSetting config,
            IBaseApiService<LoginVIewModel> baseApiService,
            IHttpContextAccessor contextAccessor)
        {
            _config = config;
            _baseApiService = baseApiService;
            _contextAccessor = contextAccessor;
        }

        public async Task<Response<LoginVIewModel>> Login(LoginVIewModel requert)
        {
            var urlApi = _config.GetApiUrl() + Constants.UrlApi.Login;
            var response = new Response<LoginVIewModel>();
            try
            {
                response = await _baseApiService.PostAsJsonAsync(urlApi, requert);

                if (response.IsSuccess)
                    SetSession(response.Value);
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return response;
        }

        public void SetSession(LoginVIewModel response)
        {
            string sessionString = JsonConvert.SerializeObject(response);
            string tokenString = JsonConvert.SerializeObject(response.AccessToken);
            if (sessionString != null)
                _contextAccessor.HttpContext.Session.SetString(Constants.SessionKey.sessionLogin, sessionString);
            if (tokenString != null)
                _contextAccessor.HttpContext.Session.SetString(Constants.SessionKey.accessToken, tokenString);
        }
    }
}
