﻿using BRS.Entities;
using BRS.Model;

namespace BRS.Services.Interface
{
    public interface IUserService
    {
        Task AddUser(UserDto userDto);
        IQueryable<Users> GetAllUsers();
      
    }
}
