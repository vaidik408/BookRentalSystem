using BRS.Data;
using BRS.Entities;
using BRS.Model;
using BRS.Repository.Interface;

namespace BRS.Repository
{
    public class RoleRepository : IRoleRepository
    {
        private readonly BRSDbContext _context;
        public RoleRepository(
            BRSDbContext context
            )
        {
            _context = context;        
        
        }

        public async Task AddRoles(RolesDto rolesDto)
        {
            

            var roles = new Roles()
            {
                 RoleName= rolesDto.RoleName,

            };
            _context.Roles.Add(roles);

            await _context.SaveChangesAsync();  
            
        }



    }
}
