using BRS.Model;
using BRS.Services.Interface;
using Microsoft.AspNetCore.Mvc;

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

        [HttpPost("AddRoles")]
        public async Task<IActionResult> AddRoles(RolesDto roleDto)
        {
            try
            {
                await _roleService.AddRoles(roleDto);
                return Ok();
            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
            
            }
        }

    }
}
