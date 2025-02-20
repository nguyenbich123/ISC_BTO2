using AuthorizationAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AuthorizationAPI.Repositories.IRepo
{
    public interface IInternRepository
    {
        Task<IEnumerable<Intern>> GetInternsByRoleAsync(int roleId);
    }
}
