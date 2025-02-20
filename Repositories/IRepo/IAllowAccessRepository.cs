using AuthorizationAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AuthorizationAPI.Repositories.IRepo
{
    public interface IAllowAccessRepository
    {
        Task<ICollection<AllowAccess>> GetAllAllowAccessAsync();
        Task<AllowAccess> GetAllowAccessByRoleIdAsync(int roleId, string table);
        Task<AllowAccess> GetAllowAccessByIdAsync(int roleId);
        Task<AllowAccess> AddAllowAccessAsync(AllowAccess allowAccess);
        Task<AllowAccess> UpdateAllowAccessAsync(AllowAccess allowAccess);
        Task DeleteAllowAccessAsync(int roleId);
    }
}
