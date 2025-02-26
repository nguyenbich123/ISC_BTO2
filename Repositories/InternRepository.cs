using AuthorizationAPI.Data;
using AuthorizationAPI.Models;
using AuthorizationAPI.Repositories.IRepo;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Dynamic;
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

        public async Task<ICollection<object>> GetInternsByRoleAsync(int roleId)
        {
            var allowAccess = await _context.AllowAccesses
                .FirstOrDefaultAsync(a => a.RoleId == roleId && a.TableName == "Interns");

            if (allowAccess == null || string.IsNullOrEmpty(allowAccess.AccessProperties))
            {
                return new List<object>();
            }

            var allowedColumns = new HashSet<string>(allowAccess.AccessProperties.Split(','));

            var interns = await _context.Interns.ToListAsync();

            var result = interns.Select(i =>
            {
                dynamic dynamicIntern = new ExpandoObject();
                var properties = typeof(Intern).GetProperties();

                foreach (var prop in properties)
                {
                    if (allowedColumns.Contains(prop.Name)) 
                    {
                        ((IDictionary<string, object>)dynamicIntern)[prop.Name] = prop.GetValue(i);
                    }
                }

                return dynamicIntern;
            }).ToList();

            return result;
        }
    }
}
