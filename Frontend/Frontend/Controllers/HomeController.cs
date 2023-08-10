using Frontend.Core.AppSettings;
using Frontend.Core.Interfaces;
using Frontend.Models;
using Frontend.Models.ViewModel.Login;
using Frontend.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;

namespace Frontend.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ILoginService _service;
        private readonly IHttpContextAccessor _contextAccessor;

        public HomeController(ILogger<HomeController> logger, ILoginService service, IHttpContextAccessor contextAccessor)
        {
            _logger = logger;
            _service = service;
            _contextAccessor = contextAccessor;
        }

        public IActionResult Index()
        {
            var sessionLogin = _contextAccessor.HttpContext.Session.GetString(Constants.SessionKey.sessionLogin);
            if (sessionLogin == null)
                return RedirectToAction("Login");

            return View();
        }

        public IActionResult Logout()
        {
            _contextAccessor.HttpContext.Session.Remove(Constants.SessionKey.sessionLogin);
            Response.Cookies.Delete(Constants.SessionKey.sessionLogin);
            Response.Clear();

            return RedirectToAction("Login");
        }

        public IActionResult Login()
        {
            var sessionLogin = _contextAccessor.HttpContext.Session.GetString(Constants.SessionKey.sessionLogin);
            if (sessionLogin != null)
                return RedirectToAction("Index");

            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(LoginResponse request)
        {
            return new JsonResult(await _service.Login(request));
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}