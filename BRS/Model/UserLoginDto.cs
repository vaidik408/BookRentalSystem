using System.ComponentModel.DataAnnotations;

namespace BRS.Model
{
    public class UserLoginDto
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string UserEmail { get; set; }

        [Required(ErrorMessage ="Password is required")]
        public string UserPassword { get; set; }
    }
}
