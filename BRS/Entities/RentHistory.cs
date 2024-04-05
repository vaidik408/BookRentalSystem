using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BRS.Entities
{
    public class RentHistory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid RentHistoryId { get; set; }
        public Guid RentId { get; set; } 
        public BookRental BookRental { get; set; }
    }
}
