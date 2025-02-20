using AuthorizationAPI.Data;
using AuthorizationAPI.Models;
using AuthorizationAPI.Repositories.IRepo;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthorizationAPI.Repositories
{
    public class InternRepository : IInternRepository
    {
        private readonly AppDbContext _context;

        public InternRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Intern>> GetInternsByRoleAsync(int roleId)
        {
            var allowAccess = await _context.AllowAccesses.FirstOrDefaultAsync(a => a.RoleId == roleId);
            if (allowAccess == null) return new List<Intern>();

            var columns = allowAccess.AccessProperties.Split(',');

            var interns = await _context.Interns.Select(i => new Intern
            {
                Id = i.Id,
                InternName = columns.Contains("InternName") ? i.InternName : null,
                DateOfBirth = columns.Contains("DateOfBirth") ? i.DateOfBirth : null,
                University = columns.Contains("University") ? i.University : null,
                Major = columns.Contains("Major") ? i.Major : null
            }).ToListAsync();

            return interns;
        }
    }
}
