using BRS.Model;

namespace BRS.Services.Interface
{
    public interface IBookRentalService
    {
        Task RentBook(Guid BookId, BookRentalDto bookRentalDto);
    }
}
