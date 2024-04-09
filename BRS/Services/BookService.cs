using BRS.Entities;
using BRS.Model;
using BRS.Repository;
using BRS.Repository.Interface;
using BRS.Services.Interface;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BRS.Services
{
    public class BookService : IBookService
    {
        private readonly IInventoryRepository _inventoryRepository;
        private readonly IBookRepository _bookRepository;
        private readonly ILogger<BookService> _logger;

        public BookService(
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
                _logger.LogError(ex, "Error occurred in AddBook method: {Message}", ex.Message);
                throw;
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
                _logger.LogError(ex, "Error occurred in GetAllBooks method: {Message}", ex.Message);
                throw;
            }
        }

        public async Task<bool> DeleteBook(Guid bookId)
        {
            try
            {
                return await _bookRepository.DeleteBook(bookId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }
    }
}
