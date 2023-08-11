using AutoMapper;
using Frontend.Core.AppSettings;
using Frontend.Core.Interfaces;
using Frontend.DTOs;
using Frontend.Models.ViewModel;
using Frontend.Models.ViewModel.Login;
using Frontend.Utilities;
using Newtonsoft.Json;
using System.Text;

namespace Frontend.Core.Services
{
    public class LoginService : ILoginService
    {
        private readonly IAppSetting _config;
        private readonly IBaseApiService<LoginResponse> _baseApiService;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IMapper _mapper;

        public LoginService(IAppSetting config, IBaseApiService<LoginResponse> baseApiService, IHttpContextAccessor contextAccessor, IMapper mapper)
        {
            _config = config;
            _mapper = mapper;
            _baseApiService = baseApiService;
            _contextAccessor = contextAccessor;
        }

        public async Task<Response<LoginResponse>> Login(LoginResponse requert)
        {
            var urlApi = _config.BaseUrlApi + Constants.UrlApi.Login;
            var response = new Response<LoginResponse>();
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

        public void SetSession(LoginResponse response)
        {
            var sessionModel = _mapper.Map<SessionDTO>(response);
            string sessionString = JsonConvert.SerializeObject(sessionModel);
            string tokenString = JsonConvert.SerializeObject(sessionModel.AccessToken);
            if (sessionString != null)
                _contextAccessor.HttpContext.Session.SetString(Constants.SessionKey.sessionLogin, sessionString);
            if (tokenString != null)
                _contextAccessor.HttpContext.Session.SetString(Constants.SessionKey.accessToken, tokenString);
        }
    }
}
