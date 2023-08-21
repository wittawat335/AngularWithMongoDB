using Demo.Domain.DTOs.Product;
using Demo.Domain.Models;
using Demo.Domain.Models.ViewModels;

namespace Demo.Core.Interfaces
{
    public interface IProductService
    {
        Task<Response<List<ProductDTO>>> GetAllAsync(ProductSearchModel filter);
        //Task<List<ProductDTO>> GetListByName(string filter);
        Task<Response<ProductDTO>> GetOneAsync(string code);
        Task<Response<ProductDTO>> AddAsync(ProductDTO model);
        Task<Response<ProductDTO>> GetByIdAsync(string id);
        Task<Response<ProductDTO>> UpdateAsync(ProductDTO model);
        Task<Response<ProductDTO>> DeleteByIdAsync(string id);
        Task<Response<ProductDTO>> DeleteListAsyncByCreateBy(string text);
    }
}
