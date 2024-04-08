using BRS.Data;
using BRS.Entities;
using BRS.Model;
using BRS.Repository.Interface;
using Microsoft.EntityFrameworkCore;

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
                await _context.Users.AddAsync( user );
                await _context.SaveChangesAsync();  
            }
            catch ( Exception ex ) {
                _logger.LogError($"User Not Added{ex.Message}");
                throw;
            }

        }
        public IQueryable<Users> GetAllUsers()
        {
            try
            {
            return  _context.Users.AsQueryable(); 
            }
            catch(Exception ex)
            {
                _logger.LogError($"Error Occured While Getting Users{ex.Message}");
                throw;
            }
        }
        public IQueryable<Users> ApplySorting(IQueryable<Users> query, string sortBy)
        {
            switch (sortBy.ToLower())
            {
                case "createdAt":
                    return query.OrderBy(u => u.CreatedAt);
                case "UserName":
                    return query.OrderBy(u => u.UserName);
                case "UserId":
                default:
                    return query.OrderBy(u => u.UserId);

            }
        }

        public async Task<string> GetAdminEmailbyUserId(Guid UserId)
        {
            try
            {
            var AdminEmail = await _context.Users.FirstOrDefaultAsync(u => u.UserId == UserId && u.RoleId == 1);

            return AdminEmail.UserEmail;
            }
            catch(Exception ex)
            { 
                _logger.LogError(ex.Message);
                throw;
            }

        }

        public async Task<string> GetCustomerEmailbyUserId(Guid UserId)
        {
            try
            {
                var AdminEmail = await _context.Users.FirstOrDefaultAsync(u => u.UserId == UserId && u.RoleId == 2);

                return AdminEmail.UserEmail;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }

        }
    }
}
