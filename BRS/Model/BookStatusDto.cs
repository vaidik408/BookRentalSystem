using BRS.Entities;
using BRS.Enum;

namespace BRS.Model
{
    public class BookStatusDto
    {
        public Guid BookId { get; set; }
        public BookStatusEnum Bk_Status { get; set; } 
    }
}
