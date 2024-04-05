using BRS.Entities;

namespace BRS.Model
{
    public class BookRentalDto
    {
        public Guid UserId { get; set; }
        public Guid BookId { get; set; }
        public DateOnly RentDate { get; set; }
        public DateOnly BookDate { get; set; }
    }
}
