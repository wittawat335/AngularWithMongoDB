using Frontend.Models.ViewModel.Login;
using Frontend.Utilities;

namespace Frontend.Core.Interfaces
{
    public interface IBaseApiService<T> where T : class
    {
        Task<Response<List<T>>> GetListAsync(string path);
        Task<Response<T>> GetAsyncById(string path);
        Task<Response<T>> InsertAsync(string path, T request);
        Task<Response<T>> PostAsync(string path, T request);
        Task<Response<T>> PostAsJsonAsync(string path, T request);
        Task<Response<T>> PutAsync(string path, T request);
        Task<Response<T>> PatchAsync(string path, T request);
        Task<Response<T>> DeleteAsync(string path);

    }
}
