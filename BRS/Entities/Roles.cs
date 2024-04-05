using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using BRS.Data;

namespace BRS.Entities
{
    public class Roles : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public ICollection<Users> Users { get; set; } = new List<Users>();
    }
}
