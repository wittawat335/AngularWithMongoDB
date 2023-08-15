using Frontend.Core.AppSettings;
using Frontend.Core.Interfaces;
using Frontend.DTOs;
using Frontend.Models.ViewModel.Product;
using Frontend.Models.ViewModel.User;
using Frontend.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace Frontend.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _service;
        private readonly IBaseApiService<UserDTO> _baseApiService;
        private readonly IBaseApiService<RoleDTO> _roleApiService;
        private readonly IAppSetting _config;

        public UserController(IUserService service, IBaseApiService<UserDTO> baseApiService, IBaseApiService<RoleDTO> roleApiService, IAppSetting config)
        {
            _service = service;
            _baseApiService = baseApiService;
            _roleApiService = roleApiService;
            _config = config;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetList()
        {
            return new JsonResult(await _baseApiService.GetListAsync(_config.BaseUrlApi + "Authentication/GetList"));
        }

        [HttpPost]
        public async Task<IActionResult> _Detail(string id, string action)
        {
            var model = new UserViewModel();
            var user = new Response<UserDTO>();
            if (id != null)
                user = await _baseApiService.GetAsyncById(_config.BaseUrlApi + string.Format("Authentication/{0}", id));

            var list = await _roleApiService.GetListAsync(_config.BaseUrlApi + "Authentication/GetRoleList");
            model.listRole = list.Value;
            model.userDTO = user.Value;
            model.action = action;

            return PartialView(model);
        }

        [HttpPost]
        public async Task<IActionResult> Save(UserViewModel model)
        {
            var response = new ResponseStatus();
            var baseUrlApi = _config.BaseUrlApi;
            try
            {
                if (model != null)
                {
                    switch (model.action)
                    {
                        case Constants.Action.New:
                            response = await _baseApiService.InsertAsync(baseUrlApi + "Authentication/Register", model.userDTO);
                            break;

                        case Constants.Action.Edit:
                            response = await _baseApiService.PutAsync(baseUrlApi + "Authentication/UpdateUser", model.userDTO);
                            break;

                        default:
                            response.Message = Constants.MessageError.CallAPI;
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return new JsonResult(response);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            return new JsonResult(await _baseApiService.DeleteAsync(_config.BaseUrlApi + string.Format("Authentication/{0}", id)));
        }
    }
}
