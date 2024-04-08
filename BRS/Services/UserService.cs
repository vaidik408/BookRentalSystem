using BRS.Entities;
using BRS.Model;
using BRS.Repository;
using BRS.Repository.Interface;
using BRS.Services.Interface;

namespace BRS.Services
{
    public class UserService : IUserService 
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<RoleService> _logger;

        public UserService
            (
            IUserRepository userRepository,
            ILogger<RoleService> logger
        )
        {
            _userRepository = userRepository;
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

                _logger.LogError($"{ex.Message}");

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
                _logger.LogError($"{ex.Message}");
                throw;
            }
        }

       

    }
}
