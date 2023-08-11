using Demo.Core.Interfaces;
using Demo.Domain.DTOs.Product;
using Demo.Domain.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Api.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _service;

        public ProductController(IProductService service)
        {
            _service = service;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _service.GetAllAsync(null));
        }

        [HttpPost("GetAll")]
        public async Task<IActionResult> Search(ProductSearchModel filter)
        {
            return Ok(await _service.GetAllAsync(filter));
        }

        [HttpGet("GetById")]
        public async Task<IActionResult> GetById(string id)
        {
            return Ok(await _service.GetByIdAsync(id));
        }

        [HttpPost("Add")]
        public async Task<IActionResult> Add(ProductDTO model)
        {
            return Ok(await _service.AddAsync(model));
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update(ProductDTO model)
        {
            return Ok(await _service.UpdateAsync(model));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            return Ok(await _service.DeleteByIdAsync(id));
        }
    }
}
