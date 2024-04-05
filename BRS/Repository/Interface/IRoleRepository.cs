using BRS.Model;

namespace BRS.Repository.Interface
{
    public interface IRoleRepository
    {
        Task AddRoles(RolesDto rolesDto);

    }
}
