using BRS.Model;

namespace BRS.Services.Interface
{
    public interface IRoleService
    {
        Task AddRoles(RolesDto roleDto);
    }
}
