using System.ComponentModel.DataAnnotations;

namespace BRS.Model
{
    public class RolesDto
    {
        [Required(ErrorMessage = "Role name is required")]
        public string RoleName { get; set; }
    }
}
