using AuthorizationAPI.Models;
using AuthorizationAPI.Repositories;
using AuthorizationAPI.Repositories.IRepo;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AuthorizationAPI.Services
{
    public class InternService
    {
        private readonly IInternRepository _internRepository;

        public InternService(IInternRepository internRepository)
        {
            _internRepository = internRepository;
        }

        public async Task<IEnumerable<Intern>> GetInternsByRoleAsync(int roleId)
        {
            return await _internRepository.GetInternsByRoleAsync(roleId);
        }
    }
}
