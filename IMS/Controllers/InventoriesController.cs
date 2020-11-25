using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IMS.Models;
using IMS.data;
using IMS.interfaces;
using Microsoft.AspNetCore.Authorization;
using IMS.Constants;

namespace IMS.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class InventoriesController : ControllerBase
    {
        private readonly IInventoryService inventoryService;

        public InventoriesController(IInventoryService service)
        {
            inventoryService = service;
        }

        // GET: api/Inventories
        [Authorize(Roles = Roles.AdminOrManager)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Inventory>>> GetAllInventories()
        {
            return await inventoryService.GetAllInventories();
        }

        // GET: api/Inventories/userId
        [Authorize(Roles = Roles.AllUsers)]
        [HttpGet("getbyUserId/{userId}")]
        public async Task<ActionResult<IEnumerable<Inventory>>> GetInventoriesByUserId(int userId)
        {
            var inventories = await inventoryService.GetInventoriesByUserId(userId);

            if (inventories == null)
            {
                return NotFound();
            }

            return inventories;
        }

        // GET: api/Inventories/5
        [Authorize(Roles = Roles.AllUsers)]
        [HttpGet("{id}")]
        public async Task<ActionResult<Inventory>> GetInventory(int id)
        {
            var inventory = await inventoryService.GetInventoryById(id);

            if (inventory == null)
            {
                return NotFound();
            }

            return inventory;
        }

        // PUT: api/Inventories/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [Authorize(Roles = Roles.AdminOrManager)]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInventory(int id, Inventory inventory)
        {
            if (id != inventory.Id)
            {
                return BadRequest();
            }

            await inventoryService.UpdateInventory(id, inventory);
            return NoContent();
        }

        // POST: api/Inventories
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [Authorize(Roles = Roles.AdminOrManager)]
        [HttpPost]
        public async Task<ActionResult<Inventory>> PostInventory(Inventory inventory)
        {
            try
            {
                await inventoryService.CreateInventory(inventory);
            }
            catch (Exception)
            {

                throw;
            }

            return CreatedAtAction("GetInventory", new { id = inventory.Id }, inventory);
        }

        // DELETE: api/Inventories/5
        [Authorize(Roles = Roles.AdminOrManager)]
        [HttpDelete("{id}")]
        public async Task<ActionResult<Inventory>> DeleteInventory(int id)
        {
            return await inventoryService.DeleteInventory(id);
        }
    }
}
