﻿using AuthorizationAPI.Models;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace AuthorizationAPI.Repositories.IRepo
{
    public interface IRoleRepository
    {
        Task<Role> GetRoleByIdAsync(int id);
        Task<ICollection<Role>> GetAllRolesAsync();
        Task<Role> AddRoleAsync(Role role);
        Task<Role> UpdateRoleAsync(Role role);
        Task DeleteRoleAsync(int id);
        Task<bool> AnyAsync(Expression<Func<Role, bool>> predicate);
    }
}
