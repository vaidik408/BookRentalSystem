using BRS.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BRS.Entities
{
    public class Users : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid UserId { get; set; }
        public int RoleId { get; set; }
        public Roles Roles { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string UserEmail { get; set; }   
        public string ContactNumber { get; set; }
        public ICollection<BookRental> BookRentals { get; set; } = new List<BookRental>();
    }
}
