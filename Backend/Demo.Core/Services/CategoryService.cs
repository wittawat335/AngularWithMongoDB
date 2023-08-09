using AutoMapper;
using Demo.Core.Interfaces;
using Demo.Domain.DTOs.Category;
using Demo.Domain.DTOs.Product;
using Demo.Domain.Models;
using Demo.Domain.Models.Collections;
using Demo.Domain.RepositoryContract;
using Demo.Domain.Utilities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Core.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IMongoRepository<Category> _repository;
        private readonly IMapper _mapper;

        public CategoryService(IMongoRepository<Category> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ResponseStatus> AddAsync(Category model)
        {
            var response = new ResponseStatus();
            try
            {
                await _repository.InsertOneAsync(_mapper.Map<Category>(model));
                response.IsSuccess = Constants.StatusData.True;
                response.Message = Constants.Msg.InsertComplete;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Exception Occurs : " + ex.Message;
            }

            return response;
        }

        public async Task<ResponseStatus> DeleteByIdAsync(string id)
        {
            var response = new ResponseStatus();
            try
            {
                await _repository.DeleteByIdAsync(id);
                response.IsSuccess = Constants.StatusData.True;
                response.Message = Constants.Msg.DeleteComplete;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Exception Occurs : " + ex.Message;
            }

            return response;
        }

        public async Task<Response<List<CategoryDTO>>> GetAllAsync()
        {
            var response = new Response<List<CategoryDTO>>();
            try
            {
                response.Value = _mapper.Map<List<CategoryDTO>>(await _repository.GetAll());
                response.IsSuccess = Constants.StatusData.True;
                response.Message = Constants.Msg.GetList;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return response;
        }

        public Task<Response<CategoryDTO>> GetByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<Response<CategoryDTO>> GetOneAsync(string code)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseStatus> UpdateAsync(CategoryDTO model)
        {
            var response = new ResponseStatus();
            try
            {
                var findId = _repository.FindById(model.Id);

                await _repository.ReplaceOneAsync(_mapper.Map(model, findId));
                response.IsSuccess = Constants.StatusData.True;
                response.Message = Constants.Msg.UpdateComplete;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Exception Occurs : " + ex.Message;
            }

            return response;
        }
    }
}
