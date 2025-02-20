using AuthorizationAPI.Data;
using AuthorizationAPI.Models;
using AuthorizationAPI.Repositories.IRepo;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AuthorizationAPI.Repositories
{
    public class AllowAccessRepository : IAllowAccessRepository
    {
        private readonly AppDbContext _context;

        public AllowAccessRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ICollection<AllowAccess>> GetAllAllowAccessAsync()
        {
            return await _context.AllowAccesses.ToListAsync();
        }

        public async Task<AllowAccess> GetAllowAccessByRoleIdAsync(int roleId, string tableName)
        {
            return await _context.AllowAccesses.FirstOrDefaultAsync(a => a.RoleId == roleId && a.TableName == tableName);
        }

        public async Task<AllowAccess> GetAllowAccessByIdAsync(int id)
        {
            return await _context.AllowAccesses.FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<AllowAccess> AddAllowAccessAsync(AllowAccess allowAccess)
        {
            await _context.AllowAccesses.AddAsync(allowAccess);
            await _context.SaveChangesAsync();
            return allowAccess;
        }

        public async Task<AllowAccess> UpdateAllowAccessAsync(AllowAccess allowAccess)
        {
            _context.AllowAccesses.Update(allowAccess);
            await _context.SaveChangesAsync();
            return allowAccess;
        }

        public async Task DeleteAllowAccessAsync(int roleId)
        {
            var allowAccess = await _context.AllowAccesses.FirstOrDefaultAsync(a => a.RoleId == roleId);
            if (allowAccess != null)
            {
                _context.AllowAccesses.Remove(allowAccess);
                await _context.SaveChangesAsync();
            }
        }

    }
}
