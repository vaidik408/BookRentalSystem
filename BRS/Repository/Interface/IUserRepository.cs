﻿using BRS.Entities;
using BRS.Model;

namespace BRS.Repository.Interface
{
    public interface IUserRepository
    {
        Task AddUser(UserDto userDto);
        IQueryable<Users> GetAllUsers();
        IQueryable<Users> ApplySorting(IQueryable<Users> query, string sortBy);
        Task<string> GetAdminEmailbyUserId(Guid UserId);
        Task<string> GetCustomerEmailbyUserId(Guid UserId);
    }
}
