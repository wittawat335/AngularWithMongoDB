using Frontend.DTOs;

namespace Frontend.Models.ViewModel.Menu
{
    public class MenuViewModel
    {
        public MenuDTO menuDTO { get; set; }
        public string action { get; set; }
        public MenuViewModel()
        {
            menuDTO = new MenuDTO();
        }
    }
}
