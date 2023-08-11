using Frontend.DTOs;
using Frontend.Models.ViewModel.Product;
using Frontend.Utilities;

namespace Frontend.Core.Interfaces
{
    public interface IProductService
    {
        Task<Response<List<ProductDTO>>> Search(string url, ProductSearch filter);
        Task<ProductViewModel> Detail(string id, string action, string url, string url2);
        Task<ResponseStatus> Save(ProductViewModel model);
        Task<ResponseStatus> Delete(string id);

    }
}
