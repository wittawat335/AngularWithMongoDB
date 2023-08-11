using Frontend.Core.AppSettings;
using Frontend.Core.Interfaces;
using Frontend.DTOs;
using Frontend.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace Frontend.ViewComponents
{
    public class MenuViewComponent : ViewComponent
    {
        private readonly IBaseApiService<MenuDTO> _baseApiService;
        private readonly IAppSetting _config;
        Common common = new Common();

        public MenuViewComponent(IBaseApiService<MenuDTO> baseApiService, IAppSetting config)
        {
            _baseApiService = baseApiService;
            _config = config;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var menu = new Response<List<MenuDTO>>();
            var loginInfo = common.GetValueBySession();
            if (loginInfo != null)
            {
                menu = await _baseApiService.GetListAsync(_config.BaseUrlApi + string.Format("menu/{0}", loginInfo.UserId));
            }

            return View(menu.Value);
        }
    }
}
