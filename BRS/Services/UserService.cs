using BRS.Entities;
using BRS.Model;
using BRS.Repository;
using BRS.Repository.Interface;
using BRS.Services.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BRS.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<UserService> _logger;
        private readonly IConfiguration _configuration;

        public UserService(
            IUserRepository userRepository,
            IConfiguration configuration,
            ILogger<UserService> logger
        )
        {
            _userRepository = userRepository;
            _configuration = configuration;
            _logger = logger;
        }

        public async Task AddUser(UserDto userDto)
        {
            try
            {
                await _userRepository.AddUser(userDto);
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
                return _userRepository.GetAllUsers();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting all users.");
                throw new Exception("Error occurred while getting all users.", ex);
            }
        }
    }
}
