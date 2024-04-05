using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BRS.Entities
{
    public class BookStatus
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid StatusId { get; set; }
        public Guid BookId { get; set; }
       public Books Books { get; set; }
        public string Bk_Status {  get; set; }
    }
}
