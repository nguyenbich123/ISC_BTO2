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

        public async Task<IEnumerable<AllowAccess>> GetAllAllowAccessAsync()
        {
            return await _context.AllowAccesses.ToListAsync();
        }

        public async Task<AllowAccess?> GetAllowAccessByRoleIdAsync(int roleId)
        {
            return await _context.AllowAccesses.FirstOrDefaultAsync(a => a.RoleId == roleId);
        }

        public async Task AddAllowAccessAsync(AllowAccess allowAccess)
        {
            await _context.AllowAccesses.AddAsync(allowAccess);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAllowAccessAsync(AllowAccess allowAccess)
        {
            _context.AllowAccesses.Update(allowAccess);
            await _context.SaveChangesAsync();
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
