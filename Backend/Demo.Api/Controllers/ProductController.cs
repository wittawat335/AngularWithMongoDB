using Demo.Core.Interfaces;
using Demo.Domain.DTOs.Product;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Api.Controllers
{
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
            return Ok(await _service.GetAllAsync());
        }

        [HttpPost("Add")]
        public async Task<IActionResult> Add(ProductInput model)
        {
            return Ok(await _service.AddAsync(model));
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update(ProductDTO model)
        {
            return Ok(await _service.UpdateAsync(model));
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(string id)
        {
            return Ok(await _service.DeleteByIdAsync(id));
        }
    }
}
