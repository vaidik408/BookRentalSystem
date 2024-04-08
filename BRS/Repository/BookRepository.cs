using BRS.Data;
using BRS.Entities;
using BRS.Model;
using BRS.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.Xml;

namespace BRS.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly BRSDbContext _context;
        private readonly ILogger<BookRepository> _logger;
        private readonly IBookStatusRepository _statusRepository;
        public BookRepository(
            BRSDbContext context,
            ILogger<BookRepository> logger,
            IBookStatusRepository bookStatusRepository
            )
        {
            _context = context;
            _logger = logger;
            _statusRepository = bookStatusRepository;
        }

        public async Task AddBooks(BookDto bookDto)
        {

            var book = new Books()
            {
                BookId = Guid.NewGuid(),
                Bk_Title = bookDto.Bk_Title,
                Bk_Number = bookDto.Bk_Number,
                Bk_Author = bookDto.Bk_Author,
                Bk_Description = bookDto.Bk_Description,
                
            };
            await _context.Books.AddAsync(book);
           await _statusRepository.AddBookStatus(book);
            await _context.SaveChangesAsync();
        }

        public IQueryable<BookStatusDto> GetAllBooks()
        {
            try
            {
                var query = _context.Books
                .Select(book => new BookStatusDto
                {
                    BookId = book.BookId,
                    Bk_Title = book.Bk_Title,
                    Bk_Number = book.Bk_Number,
                    Bk_Description = book.Bk_Description,
                    Bk_Author = book.Bk_Author,
                    Bk_Status = book.BookStatus.Bk_Status
                });

                return query;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error Occured While Getting Users{ex.Message}");
                throw;
            }
        }

        public IQueryable<BookStatusDto> ApplySorting(IQueryable<BookStatusDto> query, string sortBy, bool descending = false)
        {
            switch (sortBy.ToLower())
            { 
                case "bk_title":
                    return descending ? query.OrderByDescending(b => b.Bk_Title) : query.OrderBy(b => b.Bk_Title);
                case "bk_author":
                    return descending ? query.OrderByDescending(b => b.Bk_Author) : query.OrderBy(b => b.Bk_Author);
                case "bk_number":
                    return descending ? query.OrderByDescending(b => b.Bk_Number) : query.OrderBy(b => b.Bk_Number);
                case "bookid":
                default:
                    return descending ? query.OrderByDescending(b => b.BookId) : query.OrderBy(b => b.BookId);
            }
        }






    }
}
