using Frontend.Core.Interfaces;
using Frontend.Models;
using Frontend.Models.ViewModel.Login;
using Frontend.Utilities;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

namespace Frontend.Core.Services
{
    public class LoginService : ILoginService
    {
        HttpClientHandler _httpClientHandler = new HttpClientHandler();

        public LoginService()
        {
            _httpClientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
        }
        public async Task<Response<LoginResponse>> Login(LoginRequest model)
        {
            var response = new Response<LoginResponse>();
            try
            {
                using (var http = new HttpClient(_httpClientHandler))
                {
                    StringContent content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
                    using (var result = await http.PostAsync("https://localhost:7150/api/Authentication/Login", content))
                    {
                        string apiResult = await result.Content.ReadAsStringAsync();
                        response = JsonConvert.DeserializeObject<Response<LoginResponse>>(apiResult);
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
