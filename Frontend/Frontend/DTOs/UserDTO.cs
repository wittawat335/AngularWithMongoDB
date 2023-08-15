using System.ComponentModel.DataAnnotations;

namespace Frontend.DTOs
{
    public class UserDTO
    {
        public string Id { get; set; } = string.Empty;
        public string UserName { get; set; }
        [Required, EmailAddress]
        public string Email { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        [Required, DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;
        [Required, DataType(DataType.Password), Compare(nameof(Password), ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; } = string.Empty;
        public string IsActive { get; set; } = string.Empty;
        //public DateTime CreatedOn { get; set; }
    }
}
