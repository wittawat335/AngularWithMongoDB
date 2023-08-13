using Amazon.Runtime.Internal;
using Demo.Core.Interfaces;
using Demo.Domain.DTOs.User;
using Demo.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticateService _service;
        private readonly IAppSettings _appSettings;

        public AuthenticationController(IAuthenticateService service, IAppSettings appSettings)
        {
            _service = service;
            _appSettings = appSettings;
        }

        [HttpGet("TestEnv")]
        public IActionResult Index()
        {
            var response = new ResponseStatus();

            return Ok(response);
        }

        [HttpGet("GetList")]
        public IActionResult GetList()
        {
            return Ok(_service.GetList());
        }

        [HttpGet("GetRoleList")]
        public IActionResult GetRoleList()
        {
            return Ok(_service.GetRoleList());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            return Ok(await _service.GetByIdAsync(id));
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            return Ok(await _service.LoginAsync(request));
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            return Ok(await _service.RegisterAsync(request));
        }

        [HttpPut]
        [Route("UpdateUser")]
        public async Task<IActionResult> UpdateUser([FromBody] UserDTO request)
        {
            return Ok(await _service.UpdateUser(request));
        }

        //[Authorize(Roles = "Manager")]
        [HttpPost]
        [Route("CreateRole")]
        public async Task<IActionResult> CreateRole([FromBody] CreateRoleRequest request)
        {
            return Ok(await _service.CreateRoleAsync(request));
        }
    }
}
