using Frontend.DTOs;

namespace Frontend.Models.ViewModel.Menu
{
    public class MenuViewModel
    {
        public MenuDTO menuDTO { get; set; }
        public List<RoleDTO> listRole { get; set; }
        public List<MenuDTO> listMenu { get; set; }
        public List<RoleMenuDTO> listRoleMenu { get; set; }
        public RoleMenuDTO roleMenuDTO { get; set; }
        public string action { get; set; }
        public MenuViewModel()
        {
            menuDTO = new MenuDTO();
            roleMenuDTO = new RoleMenuDTO();
            listRole = new List<RoleDTO>();
            listMenu = new List<MenuDTO>();
            listRoleMenu = new List<RoleMenuDTO>();
        }
    }
}
