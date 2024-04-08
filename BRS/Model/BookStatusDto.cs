using BRS.Entities;
using BRS.Enum;

namespace BRS.Model
{
    public class BookStatusDto
    {
        public Guid BookId { get; set; }
        public string Bk_Title { get; set; }
        public string Bk_Number { get; set; }
        public string Bk_Description { get; set; }
        public string Bk_Author { get; set; }

        public BookStatusEnum Bk_Status { get; set; }
    }
}
