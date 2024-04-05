using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BRS.Entities
{
    public class Books
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid BookId { get; set; }
        public Guid StatusId { get; set; }
        public BookStatus BookStatus { get; set; }
        public string Bk_Title { get; set; }
        public string Bk_Number { get; set; }
        public string Bk_Name { get; set; }
        public string Bk_Description { get; set; }
        public string Bk_Author { get; set; }

        public List<BookRental> BookRental { get; set;} = new List<BookRental>();
    }
}
