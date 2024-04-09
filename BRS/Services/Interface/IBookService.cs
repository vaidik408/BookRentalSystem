using BRS.Entities;
using BRS.Model;

namespace BRS.Services.Interface
{
    public interface IBookService
    {
        Task AddBook(BookDto bookDto);
        IQueryable<BookStatusDto> GetAllBooks();
        Task<bool> DeleteBook(Guid bookId);

    }
}
