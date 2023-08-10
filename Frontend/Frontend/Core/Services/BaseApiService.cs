using Frontend.Core.AppSettings;
using Frontend.Core.Interfaces;
using Frontend.Utilities;
using Newtonsoft.Json;
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
        public async Task<Response<List<T>>> GetListAsync(string url)
        {
            var session = common.GetValueBySession();
            var response = new Response<List<T>>();
            try
            {
                using (var client = new HttpClient(_httpClientHandler))
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", session.AccessToken);
                    HttpResponseMessage result = await client.GetAsync(url);

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

        public async Task<Response<T>> GetAsyncById(string url, string id)
        {
            var session = common.GetValueBySession();
            var response = new Response<T>();
            try
            {
                using (var client = new HttpClient(_httpClientHandler))
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", session.AccessToken);
                    HttpResponseMessage result = await client.GetAsync(url + string.Format("/GetById?id={0}", id));
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

        public async Task<Response<T>> PostAsJsonAsync(T request, string url)
        {
            var response = new Response<T>();
            try
            {
                using (var client = new HttpClient(_httpClientHandler))
                {
                    client.BaseAddress = new Uri(url);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage result = await client.PostAsJsonAsync<T>(url, request);
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

        public async Task<Response<T>> PostAsync(T request, string url)
        {
            var response = new Response<T>();
            try
            {
                using (var client = new HttpClient(_httpClientHandler))
                {
                    StringContent content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
                    using (var result = await client.PostAsync(url, content))
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

        public async Task<ResponseStatus> InsertAsync(T request, string url)
        {
            var session = common.GetValueBySession();
            var response = new ResponseStatus();
            try
            {
                using (var client = new HttpClient(_httpClientHandler))
                {
                    client.BaseAddress = new Uri(url);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", session.AccessToken);
                    //StringContent content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
                    //HttpResponseMessage result = await client.PostAsync(url, content);
                    HttpResponseMessage result = await client.PostAsJsonAsync<T>(url, request);
                    if (result.IsSuccessStatusCode)
                    {
                        string data = result.Content.ReadAsStringAsync().Result;
                        response = JsonConvert.DeserializeObject<ResponseStatus>(data);
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

        public Task<Response<T>> PatchAsync(T request, string url)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseStatus> PutAsync(T requestm, string url)
        {
            var session = common.GetValueBySession();
            var response = new ResponseStatus();
            try
            {
                using (var client = new HttpClient(_httpClientHandler))
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", session.AccessToken);
                    HttpResponseMessage result = await client.PutAsJsonAsync(url, requestm);

                    if (result.IsSuccessStatusCode)
                    {
                        string data = result.Content.ReadAsStringAsync().Result;
                        response = JsonConvert.DeserializeObject<ResponseStatus>(data);
                    }
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return response;
        }

        public Task<Response<T>> DeleteAsync(string id, string url)
        {
            throw new NotImplementedException();
        }
    }
}
