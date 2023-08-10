using Frontend.DTOs;
using Frontend.Models.ViewModel.Product;
using Frontend.Utilities;

namespace Frontend.Core.Interfaces
{
    public interface IProductService
    {
        //Task<Response<List<ProductViewModel>>> GetList(string url);
        Task<ProductViewModel> GetById(string id);
        Task<ProductViewModel> Detail(string id, string action, string url, string url2);
        Task<ResponseStatus> Save(ProductViewModel model);
    }
}
