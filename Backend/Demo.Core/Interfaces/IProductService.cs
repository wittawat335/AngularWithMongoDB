using Demo.Domain.DTOs.Product;
using Demo.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Core.Interfaces
{
    public interface IProductService
    {
        Task<Response<List<ProductDTO>>> GetAllAsync();
        Task<Response<List<ProductDTO>>> GetListByCreateBy(string filter);
        Task<Response<ProductDTO>> GetOneAsync(string code);
        Task<ResponseStatus> AddAsync(ProductInput model);
        Task<Response<ProductDTO>> GetByIdAsync(string id);
        Task<ResponseStatus> UpdateAsync(ProductDTO model);
        Task<ResponseStatus> DeleteByIdAsync(string id);
        Task<ResponseStatus> DeleteListAsyncByCreateBy(string text);
    }
}
