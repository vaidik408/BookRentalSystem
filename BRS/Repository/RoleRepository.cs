using BRS.Data;
using BRS.Entities;
using BRS.Model;
using BRS.Repository.Interface;

namespace BRS.Repository
{
    public class RoleRepository : IRoleRepository
    {
        private readonly BRSDbContext _context;
        private readonly ILogger<RoleRepository> _logger;

        public RoleRepository(
            BRSDbContext context,
            ILogger<RoleRepository> logger
        )
        {
            _context = context;
            _logger = logger;
        }

        public async Task AddRoles(RolesDto rolesDto)
        {
            try
            {
                var roles = new Roles()
                {
                    RoleName = rolesDto.RoleName,
                };

                _context.Roles.Add(roles);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while adding role data.");
                throw new Exception("Error occurred while adding role data.", ex);
            }
        }
    }
}
