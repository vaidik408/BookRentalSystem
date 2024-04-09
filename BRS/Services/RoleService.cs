using BRS.Model;
using BRS.Repository.Interface;
using BRS.Services.Interface;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

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
                await _roleRepository.AddRoles(roleDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while adding roles.");
                throw new Exception("Error occurred while adding roles.", ex);
            }
        }
    }
}
