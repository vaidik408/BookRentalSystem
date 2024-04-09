using BRS.Entities;
using BRS.Services;
using BRS.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace BRS.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InventoryController : Controller
    {
        private readonly IInventoryService _inventoryService;
        private readonly ILogger<InventoryController> _logger;

        public InventoryController(IInventoryService inventoryService, ILogger<InventoryController> logger)
        {
            _inventoryService = inventoryService;
            _logger = logger;
        }
        [Authorize(Roles = "1")]
        [HttpGet]
        public async Task<IActionResult> GetInventory()
        {
            try
            {
                var inventory = await _inventoryService.GetInventory();
                return Ok(inventory);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting inventory.");
                return StatusCode(500, "An error occurred while getting inventory.");
            }
        }
        [Authorize(Roles = "1")]
        [HttpPut("updateAvailableBook")]
        public async Task<IActionResult> UpdateAvailableBook()
        {
            try
            {
                var result = await _inventoryService.UpdateAvailableBook();
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating available book count.");
                return StatusCode(500, "An error occurred while updating available book count.");
            }
        }
    }
}
