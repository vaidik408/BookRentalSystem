using BRS.Entities;
using BRS.Model;

namespace BRS.Repository.Interface
{
    public interface IUserRepository
    {
        Task AddUser(UserDto userDto);
        IQueryable<Users> GetAllUsers();
    }
}
