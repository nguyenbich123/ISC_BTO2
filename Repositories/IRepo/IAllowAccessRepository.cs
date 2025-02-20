using AuthorizationAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AuthorizationAPI.Repositories.IRepo
{
    public interface IAllowAccessRepository
    {
        Task<IEnumerable<AllowAccess>> GetAllAllowAccessAsync();
        Task<AllowAccess?> GetAllowAccessByRoleIdAsync(int roleId);
        Task AddAllowAccessAsync(AllowAccess allowAccess);
        Task UpdateAllowAccessAsync(AllowAccess allowAccess);
        Task DeleteAllowAccessAsync(int roleId);
    }
}
