using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.VisualBasic;

namespace BRS.Entities
{
    public class BookRental
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid RentId { get; set; }
        public Guid UserId { get; set; }
        public Users Users { get; set; }
        public Guid BookId { get; set; }
        public Books Books { get; set; }
        public DateOnly RentDate { get; set; }
        public DateOnly BookDate { get; set; }
        public List<RentHistory> RentHistory { get; set; } = new List<RentHistory>(); 
    }
}
