using BRS.Model;
using BRS.Repository.Interface;
using BRS.Services.Interface;

namespace BRS.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;
        private readonly ILogger<RoleService> _logger;

        public RoleService(
            IRoleRepository roleRepository,
            ILogger<RoleService> logger
            ) 
        {
        _roleRepository = roleRepository;
        _logger = logger;
        }

        public async Task AddRoles(RolesDto roleDto)
        {
            try
            {
                    await _roleRepository.AddRoles( roleDto );  
            }
            catch(Exception ex) {

                _logger.LogError($"{ex.Message}");
            
            }
        }
    }
}
