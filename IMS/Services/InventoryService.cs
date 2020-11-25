using IMS.data;
using IMS.helpers;
using IMS.interfaces;
using IMS.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IMS.Services
{
    public class InventoryService : IInventoryService
    {
        private readonly AppSettings appSettings;
        private readonly InventoryContext _context;

        public InventoryService(IOptions<AppSettings> settings, InventoryContext context)
        {
            appSettings = settings.Value;
            _context = context;
        }

        public async Task<bool> CreateInventory(Inventory inventory)
        {
            try
            {
                _context.Inventory.Add(inventory);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<Inventory> DeleteInventory(int id)
        {
            var inventory = await _context.Inventory.FindAsync(id);
            if (inventory == null)
            {
                return null;
            }

            _context.Inventory.Remove(inventory);
            await _context.SaveChangesAsync();

            return inventory;
        }

        public async Task<List<Inventory>> GetAllInventories()
        {
            return await _context.Inventory.ToListAsync();
        }

        public async Task<List<Inventory>> GetInventoriesByUserId(int id)
        {
            return await _context.Inventory.Where(item => item.UserId == id).ToListAsync();
        }

        public async Task<Inventory> GetInventoryById(int id)
        {
            return await _context.Inventory.FindAsync(id);
        }

        public async Task<bool> UpdateInventory(int id, Inventory inventory)
        {
            _context.Entry(inventory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InventoryExists(id))
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }
        }

        private bool InventoryExists(int id)
        {
            return _context.Inventory.Any(e => e.Id == id);
        }
    }
}
