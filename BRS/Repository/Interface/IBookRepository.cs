using BRS.Entities;
using BRS.Model;
using Microsoft.EntityFrameworkCore;

namespace BRS.Repository.Interface
{
    public interface IBookRepository
    {
        Task AddBooks(BookDto bookDto);
        IQueryable<BookStatusDto> GetAllBooks();
        IQueryable<BookStatusDto> ApplySorting(IQueryable<BookStatusDto> query, string sortBy, bool descending = false);
    }
}
