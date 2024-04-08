using BRS.Entities;
using BRS.Model;
using BRS.Repository;
using BRS.Repository.Interface;
using BRS.Services.Interface;

namespace BRS.Services
{
    public class BookService : IBookService
    {
        private readonly IInventoryRepository _inventoryRepository;
        private readonly IBookRepository _bookRepository;
        private readonly ILogger<BookService> _logger;

        public BookService
            (
            IBookRepository bookRepository,
            IInventoryRepository inventoryRepository,
            ILogger<BookService> logger
        )
        {
            _inventoryRepository = inventoryRepository;
            _bookRepository = bookRepository;
            _logger = logger;
        }
        public async Task AddBook(BookDto bookDto)
        {
            try
            {
                await _bookRepository.AddBooks(bookDto);
                await _inventoryRepository.UpdateInventory();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }
        public IQueryable<BookStatusDto> GetAllBooks()
        {
            try
            {
                return _bookRepository.GetAllBooks();
            }
            catch (Exception ex)    
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }




    }
}
