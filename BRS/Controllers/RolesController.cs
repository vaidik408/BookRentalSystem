using BRS.Model;
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
    public class RolesController : Controller
    {
        private readonly IRoleService _roleService;
        private readonly ILogger<RolesController> _logger;

        public RolesController(
            IRoleService roleService,
            ILogger<RolesController> logger
        )
        {
            _logger = logger;
            _roleService = roleService;
        }

        [Authorize(Roles = "1")]
        [HttpPost("AddRoles")]
        public async Task<IActionResult> AddRoles(RolesDto roleDto)
        {
            try
            {
                if (roleDto == null)
                {
                    return BadRequest("Role data is required.");
                }

                await _roleService.AddRoles(roleDto);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while adding roles: {Message}", ex.Message);
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }
    }
}
