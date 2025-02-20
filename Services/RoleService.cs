using AuthorizationAPI.Models;
using AuthorizationAPI.Repositories;
using AuthorizationAPI.Repositories.IRepo;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AuthorizationAPI.Services
{
    public class RoleService
    {
        private readonly IRoleRepository _roleRepository;

        public RoleService(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public async Task<IEnumerable<Role>> GetAllRolesAsync()
        {
            return await _roleRepository.GetAllRolesAsync();
        }

        public async Task<Role?> GetRoleByIdAsync(int id)
        {
            return await _roleRepository.GetRoleByIdAsync(id);
        }

        public async Task AddRoleAsync(Role role)
        {
            await _roleRepository.AddRoleAsync(role);
        }

        public async Task UpdateRoleAsync(Role role)
        {
            await _roleRepository.UpdateRoleAsync(role);
        }

        public async Task DeleteRoleAsync(int id)
        {
            await _roleRepository.DeleteRoleAsync(id);
        }
    }
}
