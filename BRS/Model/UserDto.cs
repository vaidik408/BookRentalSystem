using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BRS.Model
{
    public class UserDto
    {
        [Required(ErrorMessage = "Role ID is required")]
        public int RoleId { get; set; }

        [Required(ErrorMessage = "Username is required")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "The password must be at least 8 characters long.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string UserEmail { get; set; }

        [Phone(ErrorMessage = "Invalid contact number format")]
        public string ContactNumber { get; set; }
    }
}
