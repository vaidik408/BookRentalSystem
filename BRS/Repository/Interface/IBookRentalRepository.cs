using BRS.Model;

namespace BRS.Repository.Interface
{
    public interface IBookRentalRepository
    {
        Task<string> SendBookRentendNotification(Guid UserId);
        Task RentBook(Guid BookId, BookRentalDto bookRentalDto);
    }
}
