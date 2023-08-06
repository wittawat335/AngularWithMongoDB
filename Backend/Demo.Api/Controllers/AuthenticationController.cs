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
            return Ok(_appSettings.TestEnv);
        }

        [HttpPost]
        [Route("Login")]
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

        [Authorize(Roles = "Manager")]
        [HttpPost]
        [Route("CreateRole")]
        public async Task<IActionResult> CreateRole([FromBody] CreateRoleRequest request)
        {
            return Ok(await _service.CreateRoleAsync(request));
        }
    }
}
