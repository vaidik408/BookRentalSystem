using BRS.Entities;

namespace BRS.Model
{
    public class BookRentalDto
    {
        public Guid UserId { get; set; } = Guid.Empty;
        public Guid BookId { get; set; }= Guid.Empty;
        public DateOnly RentDate { get; set; }
        public DateOnly BookDate { get; set; }
    }
}
