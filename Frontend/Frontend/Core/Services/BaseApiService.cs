using Frontend.Core.AppSettings;
using Frontend.Core.Interfaces;
using Frontend.Utilities;
using Newtonsoft.Json;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace Frontend.Core.Services
{
    public class BaseApiService<T> : IBaseApiService<T> where T : class
    {
        private readonly IAppSetting _config;
        private readonly IHttpContextAccessor _contxt;
        HttpClientHandler _httpClientHandler = new HttpClientHandler();
        Common common = new Common();

        public BaseApiService(IAppSetting config, IHttpContextAccessor contxt)
        {
            _config = config;
            _httpClientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            _contxt = contxt;
        }
        public async Task<Response<List<T>>> GetListAsync(string path)
        {
            var session = common.GetValueBySession();
            var response = new Response<List<T>>();
            try
            {
                using (var client = new HttpClient(_httpClientHandler))
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", session.AccessToken);
                    HttpResponseMessage result = await client.GetAsync(path);

                    if (result.IsSuccessStatusCode)
                    {
                        string data = result.Content.ReadAsStringAsync().Result;
                        response = JsonConvert.DeserializeObject<Response<List<T>>>(data);
                    }
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return response;
        }
        public async Task<Response<T>> GetAsyncById(string path)
        {
            var session = common.GetValueBySession();
            var response = new Response<T>();
            try
            {
                using (var client = new HttpClient(_httpClientHandler))
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", session.AccessToken);
                    HttpResponseMessage result = await client.GetAsync(path);
                    if (result.IsSuccessStatusCode)
                    {
                        string data = result.Content.ReadAsStringAsync().Result;
                        response = JsonConvert.DeserializeObject<Response<T>>(data);
                    }
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return response;
        }
        public async Task<Response<T>> InsertAsync(string path, T request)
        {
            var session = common.GetValueBySession();
            var response = new Response<T>();
            try
            {
                using (var client = new HttpClient(_httpClientHandler))
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", session.AccessToken);
                    HttpResponseMessage result = await client.PostAsJsonAsync(path, request);

                    if (result.IsSuccessStatusCode)
                    {
                        string data = result.Content.ReadAsStringAsync().Result;
                        response = JsonConvert.DeserializeObject<Response<T>>(data);
                    }
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return response;
        }
        public async Task<Response<T>> PostAsJsonAsync(string path, T request)
        {
            var response = new Response<T>();
            var session = common.GetValueBySession();
            try
            {
                using (var client = new HttpClient(_httpClientHandler))
                {
                    client.BaseAddress = new Uri(path);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", session.AccessToken);
                    HttpResponseMessage result = await client.PostAsJsonAsync<T>(path, request);
                    if (result.IsSuccessStatusCode)
                    {
                        string data = result.Content.ReadAsStringAsync().Result;
                        response = JsonConvert.DeserializeObject<Response<T>>(data);
                    }
                    else
                    {
                        response.Message = Constants.MessageError.CallAPI;
                    }
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return response;
        }
        public async Task<Response<T>> PostAsync(string path, T request)
        {
            var response = new Response<T>();
            var session = common.GetValueBySession();
            try
            {
                using (var client = new HttpClient(_httpClientHandler))
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", session.AccessToken);
                    StringContent content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
                    using (var result = await client.PostAsync(path, content))
                    {
                        string apiResult = await result.Content.ReadAsStringAsync();
                        response = JsonConvert.DeserializeObject<Response<T>>(apiResult);
                    }
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return response;
        }
        public Task<Response<T>> PatchAsync(string path, T request)
        {
            throw new NotImplementedException();
        }
        public async Task<Response<T>> PutAsync(string path, T request)
        {
            var session = common.GetValueBySession();
            var response = new Response<T>();
            try
            {
                using (var client = new HttpClient(_httpClientHandler))
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", session.AccessToken);
                    HttpResponseMessage result = await client.PutAsJsonAsync(path, request);

                    if (result.IsSuccessStatusCode)
                    {
                        string data = result.Content.ReadAsStringAsync().Result;
                        response = JsonConvert.DeserializeObject<Response<T>>(data);
                    }
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return response;
        }
        public async Task<Response<T>> DeleteAsync(string path)
        {
            var session = common.GetValueBySession();
            var response = new Response<T>();
            try
            {
                using (var client = new HttpClient(_httpClientHandler))
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", session.AccessToken);
                    HttpResponseMessage result = await client.DeleteAsync(path);


                    if (result.IsSuccessStatusCode)
                    {
                        string data = result.Content.ReadAsStringAsync().Result;
                        response = JsonConvert.DeserializeObject<Response<T>>(data);
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
