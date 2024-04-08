using BRS.Entities;
using BRS.Model;
using Microsoft.EntityFrameworkCore;

namespace BRS.Repository.Interface
{
    public interface IBookRepository
    {
        Task AddBooks(BookDto bookDto);
        IQueryable<BookDto> GetAllBooks();
        IQueryable<BookDto> ApplySorting(IQueryable<BookDto> query, string sortBy, bool descending = false);
    }
}
