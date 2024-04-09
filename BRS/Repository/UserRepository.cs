using BRS.Data;
using BRS.Entities;
using BRS.Model;
using BRS.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BRS.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly BRSDbContext _context;
        private readonly ILogger<UserRepository> _logger;

        public UserRepository(
            BRSDbContext context,
            ILogger<UserRepository> logger
        )
        {
            _context = context;
            _logger = logger;
        }

        public async Task AddUser(UserDto userDto)
        {
            try
            {
                var user = new Users()
                {
                    RoleId = userDto.RoleId,
                    UserName = userDto.UserName,
                    Password = userDto.Password,
                    UserEmail = userDto.UserEmail,
                    ContactNumber = userDto.ContactNumber,
                };

                await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while adding user.");
                throw new Exception("Error occurred while adding user.", ex);
            }
        }

        public IQueryable<Users> GetAllUsers()
        {
            try
            {
                return _context.Users.AsQueryable();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting all users.");
                throw new Exception("Error occurred while getting all users.", ex);
            }
        }

        public IQueryable<Users> ApplySorting(IQueryable<Users> query, string sortBy)
        {
            try
            {
                switch (sortBy.ToLower())
                {
                    case "createdat":
                        return query.OrderBy(u => u.CreatedAt);
                    case "username":
                        return query.OrderBy(u => u.UserName);
                    case "userid":
                    default:
                        return query.OrderBy(u => u.UserId);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while applying sorting by {sortBy}.");
                throw new Exception($"Error occurred while applying sorting by {sortBy}.", ex);
            }
        }

        public async Task<string> GetAdminEmailbyUserId()
        {
            try
            {
                var adminEmail = await _context.Users
                    .Where(u => u.RoleId == 1)
                    .Select(u => u.UserEmail)
                    .FirstOrDefaultAsync();

                return adminEmail;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting admin email.");
                throw new Exception("Error occurred while getting admin email.", ex);
            }
        }

        public async Task<string> GetCustomerEmailbyUserId(Guid userId)
        {
            try
            {
                var customerEmail = await _context.Users
                    .Where(u => u.UserId == userId && u.RoleId == 2)
                    .Select(u => u.UserEmail)
                    .FirstOrDefaultAsync();

                return customerEmail;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting customer email.");
                throw new Exception("Error occurred while getting customer email.", ex);
            }
        }
      
            public async Task<UserDto> Authenticate(UserLoginDto userLogin)
            {
            try
            {
                var currentUser = await _context.Users.FirstOrDefaultAsync(o => o.UserEmail.ToLower() == userLogin.UserEmail.ToLower() && o.Password == userLogin.UserPassword);

                if (currentUser != null)
                {
                    UserDto user = new UserDto();   

                    user.UserEmail = currentUser.UserEmail;
                    user.Password = currentUser.Password;
                   user.RoleId = currentUser.RoleId;
                    
                    return user;
                }
                return null;
            }

            catch (Exception error)

            {

                _logger.LogError($"Error occured while authenticate the user");

                return null;

            }

        }
    }
}
