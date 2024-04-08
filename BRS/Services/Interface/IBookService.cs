using BRS.Entities;
using BRS.Model;

namespace BRS.Services.Interface
{
    public interface IBookService
    {
        Task AddBook(BookDto bookDto);
        IQueryable<BookDto> GetAllBooks();

    }
}
