using Frontend.DTOs;
using Frontend.Models.ViewModel.Product;
using Frontend.Utilities;

namespace Frontend.Core.Interfaces
{
    public interface IProductService
    {
        Task<Response<List<ProductDTO>>> Search(string url, ProductSearch filter);
        Task<List<ProductDTO>> Select2Product(string url, string query);
        Task<ProductViewModel> Detail(string id, string action);
        Task<ResponseStatus> Save(ProductViewModel model);
        Task<ResponseStatus> Delete(string id);
    }
}
