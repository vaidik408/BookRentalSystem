using BRS.Entities;
using BRS.Model;
using BRS.Repository;
using BRS.Repository.Interface;
using BRS.Services.Interface;

namespace BRS.Services
{
    public class BookService : IBookService
    {

        private readonly IBookRepository _bookRepository;
        private readonly ILogger<BookService> _logger;

        public BookService
            (
            IBookRepository bookRepository,
            ILogger<BookService> logger
        )
        {
            _bookRepository = bookRepository;
            _logger = logger;
        }
        public async Task AddBook(BookDto bookDto)
        {
            try
            {
                await _bookRepository.AddBooks(bookDto);
            }
            catch (Exception ex)
            {

                _logger.LogError($"{ex.Message}");

            }
        }
        public IQueryable<BookDto> GetAllBooks()
        {
            try
            {
                return _bookRepository.GetAllBooks();
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.Message}");
                throw;
            }
        }




    }
}
