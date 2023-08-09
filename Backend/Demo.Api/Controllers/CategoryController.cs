using Demo.Core.Interfaces;
using Demo.Domain.DTOs.Category;
using Demo.Domain.DTOs.Product;
using Demo.Domain.Models.Collections;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _service;

        public CategoryController(ICategoryService service)
        {
            _service = service;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _service.GetAllAsync());
        }

        [HttpPost("Add")]
        public async Task<IActionResult> Add(Category model)
        {
            return Ok(await _service.AddAsync(model));
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update(CategoryDTO model)
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
