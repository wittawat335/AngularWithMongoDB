using Demo.Core.Interfaces;
using Demo.Domain.DTOs.Menu;
using Demo.Domain.Models.Collections;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class MenuController : ControllerBase
    {
        private readonly IMenuService _service;
        public MenuController(IMenuService service)
        {
            _service = service;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _service.GetAll());
        }

        [HttpGet("{userId}")]
        public IActionResult GetList(Guid userId)
        {
            return Ok(_service.GetList(userId));
        }

        [HttpGet("GetById")]
        public async Task<IActionResult> GetById(string id)
        {
            return Ok(await _service.GetByIdAsync(id));
        }

        //[Authorize(Roles = "Manager")]
        [HttpPost("Add")]
        public async Task<IActionResult> Add(MenuInput model)
        {
            return Ok(await _service.AddAsync(model));
        }

        //[Authorize(Roles = "Manager")]
        [HttpPost("AddRoleMenu")]
        public async Task<IActionResult> AddRoleMenu(RoleMenu model)
        {
            return Ok(await _service.AddRoleManuAsync(model));
        }

        //[Authorize(Roles = "Manager")]
        [HttpPut("Update")]
        public async Task<IActionResult> Update(MenuDTO model)
        {
            return Ok(await _service.UpdateAsync(model));
        }

        [Authorize(Roles = "Manager")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            return Ok(await _service.DeleteByIdAsync(id));
        }
    }
}
