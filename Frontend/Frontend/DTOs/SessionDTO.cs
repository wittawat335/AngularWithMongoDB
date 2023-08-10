namespace Frontend.DTOs
{
    public class SessionDTO
    {
        public string AccessToken { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;
        public string fullName { get; set; }
        public string roleName { get; set; }
    }
}
