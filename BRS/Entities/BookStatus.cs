using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using BRS.Data;
using BRS.Enum;

namespace BRS.Entities
{
    public class BookStatus : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid StatusId { get; set; }
        public Guid BookId { get; set; }
        public Books Books { get; set; }
        public BookStatusEnum Bk_Status { get; set; } 
    }
}
