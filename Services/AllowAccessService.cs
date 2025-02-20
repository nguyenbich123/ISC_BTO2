using AuthorizationAPI.Models;
using AuthorizationAPI.Repositories;
using AuthorizationAPI.Repositories.IRepo;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AuthorizationAPI.Services
{
    public class AllowAccessService
    {
        private readonly IAllowAccessRepository _allowAccessRepository;

        public AllowAccessService(IAllowAccessRepository allowAccessRepository)
        {
            _allowAccessRepository = allowAccessRepository;
        }

        public async Task<IEnumerable<AllowAccess>> GetAllAllowAccessAsync()
        {
            return await _allowAccessRepository.GetAllAllowAccessAsync();
        }

        public async Task<AllowAccess?> GetAllowAccessByRoleIdAsync(int roleId)
        {
            return await _allowAccessRepository.GetAllowAccessByRoleIdAsync(roleId);
        }

        public async Task AddAllowAccessAsync(AllowAccess allowAccess)
        {
            await _allowAccessRepository.AddAllowAccessAsync(allowAccess);
        }

        public async Task UpdateAllowAccessAsync(AllowAccess allowAccess)
        {
            await _allowAccessRepository.UpdateAllowAccessAsync(allowAccess);
        }

        public async Task DeleteAllowAccessAsync(int roleId)
        {
            await _allowAccessRepository.DeleteAllowAccessAsync(roleId);
        }
    }
}
