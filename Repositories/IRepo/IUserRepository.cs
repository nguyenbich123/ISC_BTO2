﻿using AuthorizationAPI.Models;

namespace AuthorizationAPI.Repositories.IRepo
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User?> GetUserByIdAsync(int id);
        Task AddUserAsync(User user);
        Task UpdateUserAsync(User user);
        Task DeleteUserAsync(int id);
        Task<User?> GetUserByUsernameAsync(string username);

    }
}
