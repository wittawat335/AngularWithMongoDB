using Demo.Domain.DTOs.Product;
using Demo.Domain.Models;
using Demo.Domain.Models.ViewModels;

namespace Demo.Core.Interfaces
{
    public interface IProductService
    {
        Task<Response<List<ProductDTO>>> GetAllAsync(ProductSearchModel filter);
        Task<Response<List<ProductDTO>>> GetListByCreateBy(string filter);
        Task<Response<ProductDTO>> GetOneAsync(string code);
        Task<ResponseStatus> AddAsync(ProductDTO model);
        Task<Response<ProductDTO>> GetByIdAsync(string id);
        Task<ResponseStatus> UpdateAsync(ProductDTO model);
        Task<ResponseStatus> DeleteByIdAsync(string id);
        Task<ResponseStatus> DeleteListAsyncByCreateBy(string text);
    }
}
