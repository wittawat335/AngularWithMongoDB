using AutoMapper;
using Demo.Core.Interfaces;
using Demo.Domain.DTOs.Product;
using Demo.Domain.Models;
using Demo.Domain.Models.Collections;
using Demo.Domain.RepositoryContract;
using Demo.Domain.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Core.Services
{
    public class ProductService : IProductService
    {
        private readonly IMongoRepository<Products> _repository;
        private readonly IMapper _mapper;

        public ProductService(IMongoRepository<Products> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Response<List<ProductDTO>>> GetAllAsync()
        {
            var response = new Response<List<ProductDTO>>();
            try
            {
                response.Value = _mapper.Map<List<ProductDTO>>(await _repository.GetAll());
                response.IsSuccess = Constants.StatusData.True;
                response.Message = Constants.Msg.GetList;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return response;
        }

        public Task<Response<ProductDTO>> GetByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<Response<List<ProductDTO>>> GetListByCreateBy(string filter)
        {
            throw new NotImplementedException();
        }

        public Task<Response<ProductDTO>> GetOneAsync(string code)
        {
            throw new NotImplementedException();
        }
        public async Task<ResponseStatus> AddAsync(ProductInput model)
        {
            var response = new ResponseStatus();
            try
            {
                await _repository.InsertOneAsync(_mapper.Map<Products>(model));
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
        public async Task<ResponseStatus> UpdateAsync(ProductDTO model)
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

        public Task<ResponseStatus> DeleteListAsyncByCreateBy(string text)
        {
            throw new NotImplementedException();
        }
    }
}
