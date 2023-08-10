using Frontend.Models.ViewModel.Login;
using Frontend.Utilities;

namespace Frontend.Core.Interfaces
{
    public interface IBaseApiService<T> where T : class
    {
        Task<Response<List<T>>> GetListAsync(string url);
        Task<Response<T>> GetAsyncById(string url, string id);
        Task<Response<T>> PostAsync(T request, string url);
        Task<Response<T>> PostAsJsonAsync(T request, string url);
        Task<ResponseStatus> PutAsync(T request, string url);
        Task<Response<T>> PatchAsync(T request, string url);
        Task<Response<T>> DeleteAsync(string url, string id);
        Task<ResponseStatus> InsertAsync(T request, string url);
    }
}
