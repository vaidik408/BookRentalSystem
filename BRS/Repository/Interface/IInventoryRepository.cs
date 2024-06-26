﻿using BRS.Entities;

namespace BRS.Repository.Interface
{
    public interface IInventoryRepository
    {
        Task<bool> UpdateInventory();
        Task<bool> UpdateAvailableBook();
        Task<bool> UpdateReservedBook();
        Task<Inventory> GetInventory();
    }
}
