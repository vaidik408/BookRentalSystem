using BRS.Entities;
using BRS.Repository.Interface;
using BRS.Services.Interface;

namespace BRS.Services
{
    public class InventoryService : IInventoryService
    {
        private readonly IInventoryRepository _inventoryRepository;
        private readonly ILogger<InventoryService> _logger;

        public InventoryService(IInventoryRepository inventoryRepository, ILogger<InventoryService> logger)
        {
            _inventoryRepository = inventoryRepository;
            _logger = logger;
        }

        public async Task<Inventory> GetInventory()
        {
            try
            {
                var inventory = await _inventoryRepository.GetInventory();
              
                return inventory;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting inventory.");
                throw new Exception("Error occurred while getting inventory.", ex);
            }
        }

        public async Task<bool> UpdateAvailableBook()
        {
            try
            {
                var result = await _inventoryRepository.UpdateAvailableBook();
                if (!result)
                {
                    throw new Exception("Failed to update available book count.");
                }
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating available book count.");
                throw new Exception("Error occurred while updating available book count.", ex);
            }
        }
    }
}
