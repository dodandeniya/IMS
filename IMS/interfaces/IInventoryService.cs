using IMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IMS.interfaces
{
    public interface IInventoryService
    {
        Task<List<Inventory>> GetAllInventories();
        Task<List<Inventory>> GetInventoriesByUserId(int id);
        Task<Inventory> GetInventoryById(int id);
        Task<bool> UpdateInventory(int id, Inventory inventory);
        Task<bool> CreateInventory(Inventory inventory);
        Task<Inventory> DeleteInventory(int id);
    }
}
