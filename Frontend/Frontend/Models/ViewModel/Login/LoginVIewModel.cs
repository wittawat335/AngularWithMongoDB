namespace Frontend.Models.ViewModel.Login
{
    public class LoginVIewModel
    {
        public string AccessToken { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string fullName { get; set; }
        public string roleName { get; set; }
    }
}
