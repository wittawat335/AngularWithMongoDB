﻿using Demo.Domain.DTOs.Category;
using Demo.Domain.DTOs.Product;
using Demo.Domain.Models;
using Demo.Domain.Models.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Core.Interfaces
{
    public interface ICategoryService
    {
        Task<Response<List<CategoryDTO>>> GetAllAsync();
        Task<Response<CategoryDTO>> GetOneAsync(string code);
        Task<ResponseStatus> AddAsync(Category model);
        Task<Response<CategoryDTO>> GetByIdAsync(string id);
        Task<ResponseStatus> UpdateAsync(CategoryDTO model);
        Task<ResponseStatus> DeleteByIdAsync(string id);
    }
}
