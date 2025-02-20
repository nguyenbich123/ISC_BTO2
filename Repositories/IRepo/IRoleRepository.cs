using AuthorizationAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AuthorizationAPI.Repositories.IRepo
{
    public interface IRoleRepository
    {
        Task<Role?> GetRoleByIdAsync(int id);
        Task<IEnumerable<Role>> GetAllRolesAsync();
        Task AddRoleAsync(Role role);
        Task UpdateRoleAsync(Role role);
        Task DeleteRoleAsync(int id);
    }
}
