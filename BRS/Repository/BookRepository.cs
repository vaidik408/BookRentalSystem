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
                StatusId = bookDto.StatusId,
                Bk_Title = bookDto.Bk_Title,
                Bk_Number = bookDto.Bk_Number,
                Bk_Author = bookDto.Bk_Author,
                Bk_Description = bookDto.Bk_Description,
            };
            await _context.Books.AddAsync( book );  


            var status = await _statusRepository.AddBookStatus( book );


            await _context.BookStatus.AddAsync()
            await _context.SaveChangesAsync();  


        }









    }
}
