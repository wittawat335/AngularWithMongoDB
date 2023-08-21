using Frontend.DTOs;
using Frontend.Models.ViewModel.Menu;
using Frontend.Models.ViewModel.Product;
using Frontend.Utilities;

namespace Frontend.Core.Interfaces
{
    public interface IMenuService
    {
        Task<MenuViewModel> Detail(string id, string action);
        Task<string> GetMenuNameByCode(string code);
        Task<Response<MenuDTO>> Save(MenuViewModel model);
    }
}
