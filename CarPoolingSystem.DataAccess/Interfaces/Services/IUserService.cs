﻿using CarPoolingSystem.DataAccess.Entites.User;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarPoolingSystem.DataAccess.Interfaces.Services
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User?> GetUserByIdAsync(int id);
        Task AddUserAsync(User user);
        Task UpdateUserAsync(User user);
        Task DeleteUserAsync(int id);
        Task<string> UploadImage(int userId,IFormFile file);
        Task<string> GetImage(int userId);
    }
}
