using Frontend.Models.ViewModel.Menu;
using Frontend.Models.ViewModel.Product;
using Frontend.Utilities;

namespace Frontend.Core.Interfaces
{
    public interface IMenuService
    {
        Task<MenuViewModel> Detail(string id, string action);
        Task<ResponseStatus> Save(MenuViewModel model);
    }
}
