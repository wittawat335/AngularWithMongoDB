using Frontend.DTOs;

namespace Frontend.Models.ViewModel.User
{
    public class UserViewModel
    {
        public UserDTO userDTO { get; set; }
        public List<RoleDTO> listRole { get; set; }
        public string action { get; set; }
        public UserViewModel()
        {
            userDTO = new UserDTO();
            listRole = new List<RoleDTO>();
        }
    }
}
