using BRS.Model;
using BRS.Repository.Interface;
using BRS.Services;
using BRS.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BRS.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private readonly IUserService _userService;
        private readonly IUserRepository _userRepository;
        private readonly AuthorizationService _authorizationService;
        private readonly ILogger<UsersController> _logger;

        public UsersController(
            IUserService userService,
            IUserRepository userRepository,
            AuthorizationService authorizationService,
            ILogger<UsersController> logger
        )
        {
            _logger = logger;
            _userService = userService;
            _userRepository = userRepository;
            _authorizationService = authorizationService;
        }


        [HttpPost("AddUsers")]
        public async Task<IActionResult> AddUsers(UserDto userDto)
        {
            try
            {
                if (userDto == null)
                {
                    return BadRequest("User data is required.");
                }

                await _userService.AddUser(userDto);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while adding user: {Message}", ex.Message);
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }

        [Authorize(Roles = "1")]
        [HttpGet("GetUsers")]
        public IActionResult GetUsers(int page = 1, int pageSize = 10, string sortBy = "UserId", string search = "")
        {
            try
            {
                var query = _userService.GetAllUsers();

                if (!string.IsNullOrEmpty(search))
                {
                    query = query.Where(c => c.UserName.Contains(search));
                }

                query = _userRepository.ApplySorting(query, sortBy);

                var totalItems = query.Count();
                var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

                query = query.Skip((page - 1) * pageSize).Take(pageSize);

                var result = new
                {
                    TotalItems = totalItems,
                    TotalPages = totalPages,
                    Page = page,
                    PageSize = pageSize,
                    Users = query.ToList()
                };

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching users: {Message}", ex.Message);
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }

        [HttpPost("UserLogin")]
        public async Task<IActionResult> UserLogin(UserLoginDto userLogin)
        {
            try
            {
                if (userLogin == null)
                {
                    return BadRequest("User data is required.");
                }
                var user = await _userRepository.Authenticate(userLogin);
                if(user != null)
                {
                    var token = _authorizationService.Generate(user);
                    return Ok(new { Token = token });
                }
                return BadRequest("invalid email and pasword");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred during user login: {Message}", ex.Message);
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }
    }
}
