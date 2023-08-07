using Demo.Core.Interfaces;
using Demo.Core.Services;
using Demo.Domain.DTOs.Menu;
using Demo.Domain.DTOs.Product;
using Demo.Domain.Models;
using Demo.Domain.Models.Collections;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        private readonly IMenuService _service;
        public MenuController(IMenuService service)
        {
            _service = service;
        }

        [HttpGet("{userId}")]
        public IActionResult GetList(Guid userId)
        {
            return Ok(_service.GetList(userId));
        }


        [HttpPost("Add")]
        public async Task<IActionResult> Add(MenuInput model)
        {
            return Ok(await _service.AddAsync(model));
        }

        [HttpPost("AddRoleMenu")]
        public async Task<IActionResult> AddRoleMenu(RoleMenu model)
        {
            return Ok(await _service.AddRoleManuAsync(model));
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update(MenuDTO model)
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
