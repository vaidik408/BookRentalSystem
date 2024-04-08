using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.VisualBasic;
using BRS.Data;

namespace BRS.Entities
{
    public class BookRental : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid RentId { get; set; }
        public Guid UserId { get; set; } 
        public Users Users { get; set; }
        public Guid BookId { get; set; }
        public Books Books { get; set; }
        public DateOnly RentDate { get; set; }
        public DateOnly ReturnDate { get; set; }
        public List<RentHistory> RentHistory { get; set; } = new List<RentHistory>(); 
    }
}
