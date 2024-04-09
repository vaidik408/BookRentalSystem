using BRS.Entities;

namespace BRS.Services.Interface
{
    public interface IInventoryService
    {
        Task<Inventory> GetInventory();
        Task<bool> UpdateAvailableBook();
    }
}
