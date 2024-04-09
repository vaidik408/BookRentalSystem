using BRS.Data;
using BRS.Entities;
using BRS.Repository.Interface;
using Microsoft.EntityFrameworkCore;


namespace BRS.Repository
{
    public class InventoryRepository : IInventoryRepository
    {
        private readonly IBookRepository _bookRepository;
        private readonly BRSDbContext _context;
        private readonly ILogger<InventoryRepository> _logger;

        public InventoryRepository(
            IBookRepository bookRepository,
            ILogger<InventoryRepository> logger,
            BRSDbContext context
        )
        {
            _bookRepository = bookRepository;
            _logger = logger;
            _context = context;
        }

        public async Task<bool> UpdateInventory()
        {
            try
            {
                var existingInventory = await _context.Inventory.FirstOrDefaultAsync();

                var totalBooksCount = await _bookRepository.GetAllBooks().CountAsync();

                existingInventory.TotalBooks = totalBooksCount;
                existingInventory.AvailableBooks = totalBooksCount;

                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating inventory.");
                throw;
            }
        }

        public async Task<bool> UpdateReservedBook()
        {
            try
            {
                var existingInventory = await _context.Inventory.FirstOrDefaultAsync();

                existingInventory.ReservedBooks++;
                existingInventory.AvailableBooks--;

                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating reserved book count.");
                throw;
            }
        }

        public async Task<bool> UpdateAvailableBook()
        {
            try
            {
                var existingInventory = await _context.Inventory.FirstOrDefaultAsync();

                existingInventory.ReservedBooks--;
                existingInventory.AvailableBooks++;

                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating available book count.");
                throw;
            }
        }

        public async Task<Inventory> GetInventory()
        {
            try
            {
                var inventory = await _context.Inventory.FirstOrDefaultAsync();

                return inventory;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting inventory.");
                throw;
            }
        }
    }
}
