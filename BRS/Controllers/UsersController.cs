using BRS.Model;
using BRS.Services;
using BRS.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace BRS.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private readonly IUserService _userService;
        private readonly ILogger<UsersController> _logger;
        public UsersController(
                IUserService userService,
                ILogger<UsersController> logger
            )
        {
            _logger = logger;
            _userService = userService;
        }

        [HttpPost("AddUsers")]
        public async Task<IActionResult> AddRoles(UserDto userDto)
        {
            try
            {
                await _userService.AddUser(userDto);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }


        [HttpGet]
        [Route("GetConversations")]
        public IActionResult GetUsers(int page = 1, int pageSize = 10, string sortBy = "UserId", string search = "")
        {
            try
            {
                var query = _userService.GetAllUsers();

                if (!string.IsNullOrEmpty(search))
                {
                    query = query.Where(c => c.UserName.Contains(search));
                }

                query = _userService.ApplySorting(query, sortBy);

                var totalItems = query.Count();
                var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

                query = query.Skip((page - 1) * pageSize).Take(pageSize);

                var result = new
                {
                    TotalItems = totalItems,
                    TotalPages = totalPages,
                    Page = page,
                    PageSize = pageSize,
                    UserName = query.ToList()
                };

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }
    }
}
